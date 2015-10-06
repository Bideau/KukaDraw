package application;

import com.kuka.roboticsAPI.applicationModel.RoboticsAPIApplication;
import static com.kuka.roboticsAPI.motionModel.BasicMotions.*;

import com.kuka.roboticsAPI.controllerModel.Controller;
import com.kuka.roboticsAPI.deviceModel.LBR;
import com.kuka.roboticsAPI.geometricModel.Frame;
import com.kuka.roboticsAPI.geometricModel.ObjectFrame;
import com.kuka.roboticsAPI.geometricModel.Tool;
import com.kuka.roboticsAPI.geometricModel.math.Transformation;
import com.kuka.roboticsAPI.motionModel.Spline;

public class ScriptKuka extends RoboticsAPIApplication {

	// Declaration des points uitlise pour les vectors
	public double p1x,p2x,p1y,p2y,p1z,p2z;
	//public double p2xOld,p2yOld,p2zOld;
	// Rapidite : 0.2 --> 20%
	double velocity = 1;
	
	//public boolean OutPaper = false;
	//public boolean OnPaper = false;

	private Controller kuka_Sunrise_Cabinet_1;
	private LBR lbr_iiwa_14_R820_1;

	private Tool penTool;
	private ObjectFrame penToolTCP;

	private ObjectFrame paperBase;

	public ObjectFrame paperApproach;
	
	public Frame now,obj;

	public void GetLine(double _p1x, double _p1y, double _p2x, double _p2y, double _p1z, double _p2z){

		// Affecte les valeurs recuperees lors du Parsing de la trame renvoy�es par le Serveur
		this.p1x = _p1x;
		this.p2x = _p2x;
		this.p1y = _p1y;
		this.p2y = _p2y;
		this.p1z = _p1z;
		this.p2z = _p2z;
		System.out.println(this.p1x + " " + this.p1y + " " + this.p1z + " " + this.p2x + " " + this.p2y + " " + this.p2z);
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
		//this.p2xOld = 0.0;
		//this.p2yOld = 0.0;
		//this.p2zOld = 0.0;

		paperBase = getApplicationData().getFrame("/Paper");
		
		paperApproach = getApplicationData().getFrame("/Paper/PaperApproach");
	}

	private Transformation getTranslationFromFrame(Frame frameBefore, Frame frameDestination)
	{
		return Transformation.ofTranslation(
				frameDestination.getX()-frameBefore.getX(), 
				frameDestination.getY()-frameBefore.getY(), 
				frameDestination.getZ()-frameBefore.getZ()
				);
	}

	public void ApprochePaper(boolean Init){
		
		if(Init){
			// Initialisation du pencil pour ecrire avec le Kuka
			InitialisationCom();
		}
		
		// On approche la base "Paper"
		penToolTCP.move(ptp(paperApproach).setJointVelocityRel(velocity));

	}
/*
	public void initialize() {
		
	}*/
	
	public void InitialisationCom(){
		kuka_Sunrise_Cabinet_1 = getController("KUKA_Sunrise_Cabinet_1");
		lbr_iiwa_14_R820_1 = (LBR) getDevice(kuka_Sunrise_Cabinet_1,"LBR_iiwa_14_R820_1");
		penTool = getApplicationData().createFromTemplate("penTool");
		penTool.attachTo(lbr_iiwa_14_R820_1.getFlange() );
		penToolTCP = penTool.getFrame("/penToolTCP");
	}
	
	public void GetMechanicalZero(){
		lbr_iiwa_14_R820_1.move(ptpHome());
	}

	//****************************** RUN **************************//
	public void run() {
		
		long start, end;
		
		// On lance le mouvement 
		start = System.currentTimeMillis();
		//*************** Modif ABE 05/10/15 **********************//
		now = new Frame(this.p1x,this.p1y,this.p1z);
		obj = new Frame(this.p2x,this.p2y,this.p2z);
		
		penToolTCP.move((new Spline(linRel(getTranslationFromFrame(now, obj),paperBase))).setJointVelocityRel(velocity));
		
		//*********************************************//
		end = System.currentTimeMillis();
		System.out.println("LIN Move time: " + (end - start));

		// On sauvegarde le second point pour test avec prochaine mouvement
		//this.p2xOld = this.p2x;
		//this.p2yOld = this.p2y;
		
		System.out.println("------------------------------------------");
	}
}
