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


/**
 * Test de trajectoire spline construite en "code" au lieu de charger les données en dur. 
 * 
 */
public class TestSpl extends RoboticsAPIApplication {
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
	
	private Transformation getTranslationFromFrame(Frame frameBefore, Frame frameDestination)
	{
		return Transformation.ofTranslation(
				frameDestination.getX()-frameBefore.getX(), 
				frameDestination.getY()-frameBefore.getY(), 
				frameDestination.getZ()-frameBefore.getZ()
				);
	}
	
	public void initialize() {
		kuka_Sunrise_Cabinet_1 = getController("KUKA_Sunrise_Cabinet_1");
		lbr_iiwa_14_R820_1 = (LBR) getDevice(kuka_Sunrise_Cabinet_1,
				"LBR_iiwa_14_R820_1");
		
		//  On crée l'outil stylo, on l'attache au flange et on récupére le point en bout de stylo "penToolTCP"
		penTool = getApplicationData().createFromTemplate("penTool");
		penTool.attachTo(lbr_iiwa_14_R820_1.getFlange() );
		penToolTCP = penTool.getFrame("/penToolTCP");
		
		
		// On charge les points de l'application
		paperBase = getApplicationData().getFrame("/Paper");
		
		nearPaper0 = getApplicationData().getFrame("/Paper/NearPaper0");
		paperApproach = getApplicationData().getFrame("/Paper/PaperApproach");
	
	
		// On crée un courbe proche d'un sinus
		Vector2 p0 = new Vector2();
		Vector2 p1 = new Vector2();
		Vector2 p2 = new Vector2();
		Vector2 p3 = new Vector2();
		
		p0.x = 0.0;
		p0.y = 0.0;
		
		p1.x = 80.0;
		p1.y = 100.0;
		
		p2.x = 160.0;
		p2.y = -100.0;
		
		p3.x = 240.0;
		p3.y = 0.0;

		
		curve = new BezierCurve(p0, p1, p2, p3);
	
		trajectory = curve.getTrajectory(20);
		
		// On crée des frames robot Kuka depuis notre courbe
		frames = new Frame[trajectory.length];
		for (int i=0; i < trajectory.length; i++)
		{
//			getLogger().info("" + trajectory[i].x + " "+ trajectory[i].y);
			
			frames[i] = new Frame(trajectory[i].x, trajectory[i].y, 0);
		}
		
	}

	public void run() {
		lbr_iiwa_14_R820_1.move(ptpHome());
		
		double velocity = 0.2;
		
		// On approche la base "Paper"
		penToolTCP.move(
				ptp(paperApproach).setJointVelocityRel(velocity)
			);
		
		penToolTCP.move(
				lin(nearPaper0).setJointVelocityRel(velocity)
			);
		
		
		// On crée notre spline avec des mouvements relatifs linéaire dans la base "Paper"
		// Dans cet exemple :
		// - On est au point nearPaper1, à 70mm au dessus du papier
		// - On a moins de 500 mouvements 
		// - On crée les mouvements sans changer la translation sur l'axe Z
		// - Si on veux réellement dessiner, il faudrait bouger en P0 puis créer la spline
		
		// Dans un cas ou il y a plus de 500 mouvements, il faut envoyer 500 mouvements par 500 mouvements :
		// - Le bras ne prend pas plus de 500 mouvements par spline
		
		
		RelativeLIN [] splineArray = new RelativeLIN[frames.length-1];

//		SPL [] splArray = new SPL[frames.length-1];
		
//		Frame startFrame = lbr_iiwa_14_R820_1.getCurrentCartesianPosition(nearPaper1);
		
		for (int i=0; i < frames.length-1; i++)
		{
			RelativeLIN moveLin = linRel(getTranslationFromFrame(frames[i], frames[i+1]),paperBase);
			
			splineArray[i] = moveLin;
			
			// mouvement SPL ne marche pas ?
//			Frame f = (new Frame(paperP4)).setX(frames[i].getX())
//									.setY(frames[i].getY())
//									.setZ(70);
//			SPL moveSpl = spl(f);

			// test mouvement SPL :(
//			Frame f = new Frame(nearPaper1, getTranslationFromFrame(frames[i], frames[i+1]));
//			frames[i].getX(), frames[i].getY(), nearPaper1.getZ()
//			SPL moveSpl = spl(f);
			
//			splArray[i] = moveSpl;
		}
		
		
		Spline linMovement = new Spline(splineArray);
//		Spline splMovement = new Spline(splArray);
		
		long start, end;
		
		// On lance le mouvement 
		start = System.currentTimeMillis();
		penToolTCP.move(
					linMovement.setJointVelocityRel(velocity)
				);
		end = System.currentTimeMillis();
		getLogger().info("LIN Move time: " + (end - start));
		
		
		//On revient au départ
		penToolTCP.move(
				lin(nearPaper0).setJointVelocityRel(velocity)
			);
		
		
		// Et on test le mouvement SPL
/*		start = System.currentTimeMillis();
				penToolTCP.move(
					splMovement.setJointVelocityRel(velocity)
				);
		end = System.currentTimeMillis();
		getLogger().info("SPL Move time: " + (end - start));
*/	
	
	}

	/**
	 * Auto-generated method stub. Do not modify the contents of this method.
	 */
	public static void main(String[] args) {
		TestSpl app = new TestSpl();
		app.runApplication();
	}
}
