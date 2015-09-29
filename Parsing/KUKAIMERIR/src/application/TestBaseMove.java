package application;


import com.kuka.generated.ioAccess.MediaFlangeIOGroup;
import com.kuka.roboticsAPI.applicationModel.RoboticsAPIApplication;
import static com.kuka.roboticsAPI.motionModel.BasicMotions.*;
import com.kuka.roboticsAPI.controllerModel.Controller;
import com.kuka.roboticsAPI.controllerModel.sunrise.ISafetyState;
import com.kuka.roboticsAPI.deviceModel.LBR;
import com.kuka.roboticsAPI.deviceModel.OperationMode;
import com.kuka.roboticsAPI.geometricModel.CartDOF;
import com.kuka.roboticsAPI.geometricModel.ObjectFrame;
import com.kuka.roboticsAPI.geometricModel.Tool;
import com.kuka.roboticsAPI.geometricModel.math.Transformation;
import com.kuka.roboticsAPI.motionModel.Spline;
import com.kuka.roboticsAPI.motionModel.SplineJP;
import com.kuka.roboticsAPI.motionModel.controlModeModel.CartesianSineImpedanceControlMode;


/* Test des mouvements du robot Kuka avec la base "Paper"
 * 
 */

public class TestBaseMove extends RoboticsAPIApplication {
	private Controller kuka_Sunrise_Cabinet_1;
	private LBR lbr_iiwa_14_R820_1;

	private MediaFlangeIOGroup ioFlange;
	
	private CartesianSineImpedanceControlMode impedanceControlMode;
	
	private Tool penTool;
	private ObjectFrame penToolTCP;
	
	
	private ObjectFrame exampleP1;
	private ObjectFrame exampleP2;
	private ObjectFrame exampleP3;
	
	private ObjectFrame paperBase;
	private ObjectFrame paperP1;
	private ObjectFrame paperP2;
	private ObjectFrame paperP3;
	private ObjectFrame paperP0;
	
	private ObjectFrame nearPaper0;
	private ObjectFrame paperApproach;
	
	
	private Transformation getTranslationWithSpecifiedZ(ObjectFrame frameBefore, ObjectFrame frameDestination, double z)
	{
		return Transformation.ofTranslation(
				frameDestination.getX()-frameBefore.getX(), 
				frameDestination.getY()-frameBefore.getY(), 
				z
				);
	}
	
	private Transformation getTranslationFromFrame(ObjectFrame frameBefore, ObjectFrame frameDestination)
	{
		return Transformation.ofTranslation(
				frameDestination.getX()-frameBefore.getX(), 
				frameDestination.getY()-frameBefore.getY(), 
				frameDestination.getZ()-frameBefore.getZ()
				);
	}
	
	public void initialize() {
		kuka_Sunrise_Cabinet_1 = getController("KUKA_Sunrise_Cabinet_1");
		lbr_iiwa_14_R820_1 = (LBR) getDevice(kuka_Sunrise_Cabinet_1, "LBR_iiwa_14_R820_1");

		ioFlange = new MediaFlangeIOGroup(kuka_Sunrise_Cabinet_1);
	
		// On crée la compliance pour rendre le bras "mou"
		impedanceControlMode = new CartesianSineImpedanceControlMode();
		impedanceControlMode.parametrize(CartDOF.X).setStiffness(3000);
		impedanceControlMode.parametrize(CartDOF.Y).setStiffness(3000);
		impedanceControlMode.parametrize(CartDOF.Z).setStiffness(3000);
		
		impedanceControlMode.parametrize(CartDOF.A).setStiffness(200);
		impedanceControlMode.parametrize(CartDOF.B).setStiffness(200);
		impedanceControlMode.parametrize(CartDOF.C).setStiffness(200);
		
		// On crée l'outil stylo, on l'attache au flange et on récupére le point en bout de stylo "penToolTCP"
		penTool = getApplicationData().createFromTemplate("penTool");
		penTool.attachTo(lbr_iiwa_14_R820_1.getFlange() );
		penToolTCP = penTool.getFrame("/penToolTCP");
		
		
		// On charge les points de l'application
		paperBase = getApplicationData().getFrame("/Paper");
		paperP1 = getApplicationData().getFrame("/Paper/P1");
		paperP2 = getApplicationData().getFrame("/Paper/P2");
		paperP3 = getApplicationData().getFrame("/Paper/P3");
		paperP0 = getApplicationData().getFrame("/Paper/P0");
		
		nearPaper0 = getApplicationData().getFrame("/Paper/NearPaper0");
		paperApproach = getApplicationData().getFrame("/Paper/PaperApproach");
		
		exampleP1 = getApplicationData().getFrame("/Example/P1");
		exampleP2 = getApplicationData().getFrame("/Example/P2");
		exampleP3 = getApplicationData().getFrame("/Example/P3");
		
		
		getLogger().info("Initialization OK");
	}

	public void run() {
		
		double velocity = 0.2;
		
		ISafetyState currentState = lbr_iiwa_14_R820_1.getSafetyState();
		OperationMode mode = currentState.getOperationMode();
		if (mode==OperationMode.AUT)
		{
			//******* ATTENTION : AVANT D'ACTIVE LE MODE AUTO, VERIFIER LES TRAJECTOIRES ********//
			// Si on est en mode AUTO, pleine vitesse activée
			
			velocity = 1;
		}
		
		lbr_iiwa_14_R820_1.move(ptpHome());
		
		
		ioFlange.setLEDBlue(true);
		
		// Mouvement "point-to-point" dans la base "World"
		SplineJP moveToP1 = new SplineJP(
				ptp(exampleP3),
				ptp(exampleP2),
				ptp(exampleP1)
			);
		
		getLogger().info("Move P3-P2-P1");
		
		lbr_iiwa_14_R820_1.move(
				moveToP1.setJointVelocityRel(velocity).setMode(impedanceControlMode)
			);
		
		
		// Approche de la base "Paper" en PTP puis en LIN
		
		getLogger().info("Move near Paper");
				
		penToolTCP.move(
				ptp(paperApproach).setJointVelocityRel(velocity)
			);
		
		penToolTCP.move(
				lin(nearPaper0).setJointVelocityRel(velocity)
			);
		
		
		// On dessine au dessus du papier les points nearPaper1 / P0 / P1 / P2 / P3 :
		
		getLogger().info("Move on Paper");
		Spline drawingSpline = new Spline(
				
					// On bouge en relatif
					// Premier mouvement, on va en de nearPaper1 vers P0, à +10mm au dessus de la surface
					linRel( getTranslationWithSpecifiedZ(nearPaper0, paperP0, paperP0.getZ() - nearPaper0.getZ() +10 ), paperBase),
					
					// On va ensuite à P1, P2, P3 et P0, en spécifiant une translation Z nulle 
					linRel( getTranslationWithSpecifiedZ(paperP0, paperP1, 0), paperBase),
					linRel( getTranslationWithSpecifiedZ(paperP1, paperP2, 0), paperBase),
					linRel( getTranslationWithSpecifiedZ(paperP2, paperP3, 0), paperBase),
					linRel( getTranslationWithSpecifiedZ(paperP3, paperP0, 0), paperBase)
				);
		
		
		penToolTCP.move(
				drawingSpline.setJointVelocityRel(velocity)
				.setJointAccelerationRel(1)
				.setMode(impedanceControlMode)
			);
		
		// On reléve la pointe du stylo
		penToolTCP.move( 
				linRel( getTranslationFromFrame(paperP0, nearPaper0), paperBase) 
			);
		
		
		// On revient à la "maison"
		getLogger().info("Go back to home");
		
		penToolTCP.move( lin(paperApproach).setJointVelocityRel(velocity));
		
		SplineJP moveBackToHome = new SplineJP( ptpHome());
		
		getLogger().info("Move Back");
		lbr_iiwa_14_R820_1.move(
				moveBackToHome.setJointVelocityRel(velocity)
			);
		
		ioFlange.setLEDBlue(false);
	}

	
	public static void main(String[] args) {
		TestBaseMove app = new TestBaseMove();
		app.runApplication();
	}
}
