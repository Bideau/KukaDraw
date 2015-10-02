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

	// Déclaration des points uitlisé pour les vectors
	private double p1x,p2x,p1y,p2y,p2xOld,p2yOld;
	// Rapidité : 0.2 --> 20%
	double velocity = 0.2;
	private boolean HaveLine = false;

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

	public void GetLine(double _p1x, double _p1y, double _p2x, double _p2y){

		// Affecte les valeurs recuperees lors du Parsing de la trame renvoyées par le Serveur
		this.p1x = _p1x;
		this.p2x = _p2x;
		this.p1y = _p1y;
		this.p2y = _p2y;
		HaveLine = true;

		System.out.println("GETLINE FINIT");
	}

	public ScriptKuka(){

		// Constructeur ...
		// On initialise les variables
		this.p1x = 0.0;
		this.p2x = 0.0;
		this.p1y = 0.0;
		this.p2y = 0.0;
		this.p2xOld = 0.0;
		this.p2yOld = 0.0;

		kuka_Sunrise_Cabinet_1 = getController("KUKA_Sunrise_Cabinet_1");
		lbr_iiwa_14_R820_1 = (LBR) getDevice(kuka_Sunrise_Cabinet_1,"LBR_iiwa_14_R820_1");
		//  On crée l'outil stylo, on l'attache au flange et on récupére le point en bout de stylo "penToolTCP"
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

		// On crée un courbe proche d'un sinus
		Vector2 p0 = new Vector2();
		Vector2 p1 = new Vector2();

		// On affecte les valeurs reçu dans le vector pour le tracage du Kuka
		p0.x = this.p1x;
		p0.y = this.p1y;

		p1.x = this.p2x;
		p1.y = this.p2y;

		// Dans le cas ou le deuxième point du dernier mouvement est le même que le premier nouveau point ...
		// ... on ne releve pas la pointe du stylo pour continuer de dessiner.

		if(HaveLine){
			System.out.println("HaveLine");
			if(this.p1x == this.p2xOld && this.p1y == this.p2yOld){
				System.out.println("Go Papier");
				p0.z = 0;
				p1.z = 50;
			}else{
				System.out.println("Out Paper");
				p0.z = 0;
				p1.z = 50;
			}
		}

		curve = new BezierCurve(p0, p1);
		System.out.println("BEZIER CURVE");

		trajectory = curve.getTrajectory(2);
		System.out.println("GETTRAJECTORY");

		// On crée des frames robot Kuka depuis notre courbe
		frames = new Frame[trajectory.length];
		for (int i=0; i < trajectory.length; i++){
			frames[i] = new Frame(trajectory[i].x, trajectory[i].y, trajectory[i].z);
		}
		System.out.println("END INITIALIZE");
	}

	//****************************** RUN **************************//
	public void run() {
		System.out.println("Run");
		// TODO Stub de la méthode généré automatiquement
		//lbr_iiwa_14_R820_1.move(ptpHome());

		// On crée notre spline avec des mouvements relatifs linéaire dans la base "Paper"
		// Dans cet exemple :
		// - On est au point nearPaper1, à 70mm au dessus du papier
		// - On a moins de 500 mouvements 
		// - On crée les mouvements sans changer la translation sur l'axe Z
		// - Si on veux réellement dessiner, il faudrait bouger en P0 puis créer la spline

		// Dans un cas ou il y a plus de 500 mouvements, il faut envoyer 500 mouvements par 500 mouvements :
		// - Le bras ne prend pas plus de 500 mouvements par spline

		RelativeLIN [] splineArray = new RelativeLIN[frames.length-1];

		for (int i=0; i < frames.length-1; i++){
			System.out.println("X" + frames[i].getX());
			System.out.println("Y" + frames[i].getY());
			System.out.println("Z" + frames[i].getZ());
			RelativeLIN moveLin = linRel(getTranslationFromFrame(frames[i], frames[i+1]),paperBase);
			splineArray[i] = moveLin;
		}

		Spline linMovement = new Spline(splineArray);

		System.out.println("END SPLINE");

		long start, end;

		// On lance le mouvement 
		start = System.currentTimeMillis();
		System.out.println("1");
		penToolTCP.move(linMovement.setJointVelocityRel(velocity));
		System.out.println("2");
		end = System.currentTimeMillis();
		System.out.println("3");
		getLogger().info("LIN Move time: " + (end - start));
		System.out.println("LIN Move time: " + (end - start));

		//On revient au départ
		//penToolTCP.move(lin(nearPaper0).setJointVelocityRel(velocity));

		// On sauvegarde le second point pour test avec prochaine mouvement
		this.p2xOld = this.p2x;
		this.p2yOld = this.p2y;
	}
	//*****************************************************************//
}
