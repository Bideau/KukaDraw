package application;

import com.kuka.roboticsAPI.applicationModel.RoboticsAPIApplication;
import static com.kuka.roboticsAPI.motionModel.BasicMotions.*;

import com.kuka.roboticsAPI.controllerModel.Controller;
import com.kuka.roboticsAPI.deviceModel.LBR;
import com.kuka.roboticsAPI.geometricModel.Frame;
import com.kuka.roboticsAPI.geometricModel.ObjectFrame;
import com.kuka.roboticsAPI.geometricModel.Tool;
import com.kuka.roboticsAPI.geometricModel.math.Transformation;
import com.kuka.roboticsAPI.motionModel.RelativeLIN;
import com.kuka.roboticsAPI.motionModel.Spline;

public class ScriptKuka extends RoboticsAPIApplication {

	// D�claration des points uitlis� pour les vectors
	public double p1x,p2x,p1y,p2y,p1z,p2z,p2xOld,p2yOld,p2zOld;
	// Rapidit� : 0.2 --> 20%
	double velocity = 0.2;
	public boolean HaveLine = false;
	public boolean OutPaper = false;
	public boolean OnPaper = false;

	private Controller kuka_Sunrise_Cabinet_1;
	private LBR lbr_iiwa_14_R820_1;

	private Tool penTool;
	private ObjectFrame penToolTCP;

	private ObjectFrame paperBase;

	private ObjectFrame nearPaper0;
	private ObjectFrame paperApproach;

	private BezierCurve curve;
	private Vector2[] trajectory;
	private Frame[] frames;

	public void GetLine(double _p1x, double _p1y, double _p2x, double _p2y, double _p1z, double _p2z){

		// Affecte les valeurs recuperees lors du Parsing de la trame renvoy�es par le Serveur
		this.p1x = _p1x;
		this.p2x = _p2x;
		this.p1y = _p1y;
		this.p2y = _p2y;
		this.p1z = _p1z;
		this.p2z = _p2z;
		HaveLine = true;
		System.out.println(this.p1x + " " + this.p1y + " " + this.p1z + " " + this.p2x + " " + this.p2y + " " + this.p2z);
		System.out.println("GETLINE FINIT");
	}

	public ScriptKuka(){

		// Constructeur ...
		// On initialise les variables
		this.p1x = 0.0;
		this.p2x = 0.0;
		this.p1y = 0.0;
		this.p2y = 0.0;
		this.p1z = 0.0;
		this.p2z = 0.0;
		this.p2xOld = 0.0;
		this.p2yOld = 0.0;
		this.p2zOld = 0.0;

		kuka_Sunrise_Cabinet_1 = getController("KUKA_Sunrise_Cabinet_1");
		lbr_iiwa_14_R820_1 = (LBR) getDevice(kuka_Sunrise_Cabinet_1,"LBR_iiwa_14_R820_1");
		//  On cr�e l'outil stylo, on l'attache au flange et on r�cup�re le point en bout de stylo "penToolTCP"
		penTool = getApplicationData().createFromTemplate("penTool");
		//System.out.println("1");
		penTool.attachTo(lbr_iiwa_14_R820_1.getFlange() );
		//System.out.println("2");
		penToolTCP = penTool.getFrame("/penToolTCP");
		//System.out.println("3");
		// On charge les points de l'application
		paperBase = getApplicationData().getFrame("/Paper");
		//System.out.println("4");

		nearPaper0 = getApplicationData().getFrame("/Paper/NearPaper0");
		paperApproach = getApplicationData().getFrame("/Paper/PaperApproach");

		//System.out.println("END GETFRAME");
	}

	private Transformation getTranslationFromFrame(Frame frameBefore, Frame frameDestination)
	{
		return Transformation.ofTranslation(
				frameDestination.getX()-frameBefore.getX(), 
				frameDestination.getY()-frameBefore.getY(), 
				frameDestination.getZ()-frameBefore.getZ()
				);
	}

	public void ApprochePaper(){

		// On approche la base "Paper"
		penToolTCP.move(ptp(paperApproach).setJointVelocityRel(velocity));

		penToolTCP.move(lin(nearPaper0).setJointVelocityRel(velocity));
	}

	public void initialize() {

		System.out.println("Initialize");

		// On cr�e un courbe proche d'un sinus
		Vector2 p0 = new Vector2();
		Vector2 p1 = new Vector2();
		

		// On affecte les valeurs re�u dans le vector pour le tracage du Kuka
		p0.x = this.p1x;
		p0.y = this.p1y;
		p0.z = this.p1z;

		p1.x = this.p2x;
		p1.y = this.p2y;
		p1.z = this.p2z;

		curve = new BezierCurve(p0, p1);
		System.out.println("BEZIER CURVE");
		
		System.out.println("Curve : " + curve.points[0].x + "  " + curve.points[0].y + "  " + curve.points[0].z);
		System.out.println("Curve : " + curve.points[1].x + "  " + curve.points[1].y + "  " + curve.points[1].z);

		trajectory = curve.getTrajectory(2);
		System.out.println("GET TRAJECTORY");

		// On cr�e des frames robot Kuka depuis notre courbe
		frames = new Frame[trajectory.length];
		for (int i=0; i < trajectory.length; i++){
			frames[i] = new Frame(trajectory[i].x, trajectory[i].y, trajectory[i].z);
			System.out.println(trajectory[i].x + "  " + trajectory[i].y + "  " + trajectory[i].z);
		}
		System.out.println("END INITIALIZE");
	}

	//****************************** RUN **************************//
	public void run() {
		System.out.println("Run");
		// TODO Stub de la m�thode g�n�r� automatiquement
		//lbr_iiwa_14_R820_1.move(ptpHome());

		// On cr�e notre spline avec des mouvements relatifs lin�aire dans la base "Paper"
		// Dans cet exemple :
		// - On est au point nearPaper1, � 70mm au dessus du papier
		// - On a moins de 500 mouvements 
		// - On cr�e les mouvements sans changer la translation sur l'axe Z
		// - Si on veux r�ellement dessiner, il faudrait bouger en P0 puis cr�er la spline

		// Dans un cas ou il y a plus de 500 mouvements, il faut envoyer 500 mouvements par 500 mouvements :
		// - Le bras ne prend pas plus de 500 mouvements par spline

		RelativeLIN [] splineArray = new RelativeLIN[frames.length-1];

		for (int i=0; i < frames.length-1; i++){
			System.out.println(i + " X : " + frames[i].getX());
			System.out.println(i + " Y : " + frames[i].getY());
			System.out.println(i + " Z : " + frames[i].getZ());
			RelativeLIN moveLin = linRel(getTranslationFromFrame(frames[i], frames[i+1]),paperBase);
			splineArray[i] = moveLin;
		}

		Spline linMovement = new Spline(splineArray);

		System.out.println("END SPLINE");

		long start, end;
		
		// On lance le mouvement 
		start = System.currentTimeMillis();
		System.out.println("1");
		System.out.println(velocity);
		penToolTCP.move(linMovement.setJointVelocityRel(velocity));
		//penToolTCP.
		System.out.println("2");
		end = System.currentTimeMillis();
		System.out.println("3");
		getLogger().info("LIN Move time: " + (end - start));
		System.out.println("LIN Move time: " + (end - start));

		//On revient au d�part
		//penToolTCP.move(lin(nearPaper0).setJointVelocityRel(velocity));

		// On sauvegarde le second point pour test avec prochaine mouvement
		this.p2xOld = this.p2x;
		this.p2yOld = this.p2y;
		
		//Thread.interrupted();
	}
	//*****************************************************************//
}
