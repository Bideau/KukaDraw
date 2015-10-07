package Parsing;

import com.kuka.roboticsAPI.applicationModel.RoboticsAPIApplication;

import server.Server;
import application.ScriptKuka;

public class SocketTrameParsing extends RoboticsAPIApplication {

	private String Trame;
	private final double ONPAPER=-3.0;
	private final double OFFPAPER=15.0;

	// Quand on recoit un STOP
	private boolean StopTrame = false;
	// Quand le serveur perd la connexion avec le client
	private boolean Disconect = true;
	// Déclaration des points à envoyer au Kuka
	private double p1x;
	private double p1y;
	private double p2x;
	private double p2y;
	private double p1z;
	private double p2z;
	private double pPosx,pPosy,pPosz;
	// Compteur des trames ne contenant pas LINE
	private int compteurFausseTrame;
	private int compteurTrameIdentique;

	private Server MyServer;
	private ScriptKuka MonScriptKuka;

	
	//************************** CONSTRUCTEUR ************************//
	public SocketTrameParsing(){
		
	}

	public SocketTrameParsing(Server ServerParametre){

		this.MyServer = ServerParametre;
		this.MonScriptKuka = new ScriptKuka();
		
		// Initialisation des coordonnï¿½es des diffï¿½rents points
		this.p1x = 0.0;
		this.p1y = 0.0;
		this.p1z = 0.0;
		this.p2x = 0.0;
		this.p2y = 0.0;
		this.p2z = 0.0;
		this.compteurFausseTrame = 0;
		this.pPosx = MonScriptKuka.paperApproach.getX();
		this.pPosy = MonScriptKuka.paperApproach.getY();
		this.pPosz = MonScriptKuka.paperApproach.getZ();

		// Initialisation de la trame de comunication
		this.Trame = "Default";

		// Initialisation du booleen permettant de stopper la lecture dans la socket
		this.StopTrame = false;

	}
	//***************************************************************//
	
	
	//*********************** TRAITEMENT TRAME *********************//
	public void TraitementTrame(){

		// On recuepere l'ordre donne par l'IHM
		// On recherche LINE dans la chaine recupere
		int positionOrdreSTOP = this.Trame.indexOf("STOP");
		int positionOrdreSTART = this.Trame.indexOf("START");

		// Si on recoit STOP
		if (positionOrdreSTOP != -1){
			this.MonScriptKuka.ApprochePaper();
			StopTrame = true;
			compteurFausseTrame = 0;
		}

		// Si on recoit START
		if (positionOrdreSTART != -1){
			this.MonScriptKuka.ApprochePaper();
			this.pPosx = MonScriptKuka.paperApproach.getX();
			this.pPosy = MonScriptKuka.paperApproach.getY();
			this.pPosz = MonScriptKuka.paperApproach.getZ();
			StopTrame = false;
		}

		// Si on a recu un arret de lecture
		if(!StopTrame){
			// On recupere l'ordre donne par l'IHM
			// On recherche LINE dans la chaine recupere
			int positionOrdreLINE = this.Trame.indexOf("LINE");
			// Si on trouve LINE dans la chaine
			if(positionOrdreLINE != -1){

				// Chaine LINE trouve !
				// On recherche les coordonnees des deux points
				String[] GeneralParts = this.Trame.split(":");

				// On parse les coordonnï¿½es x et y du 1er point
				String[] CoordonneesPoint1 = GeneralParts[1].split(";");

				this.p1x = Double.parseDouble(CoordonneesPoint1[0]);
				this.p1y = Double.parseDouble(CoordonneesPoint1[1]);

				// On parse les coordonnï¿½es x et y du 2eme point
				String[] CoordonneesPoint2 = GeneralParts[2].split(";");

				this.p2x = Double.parseDouble(CoordonneesPoint2[0]);
				this.p2y = Double.parseDouble(CoordonneesPoint2[1]);

				// Dans le cas ou le deuxieme point du dernier mouvement est le meme que le premier nouveau point ...
				// ... on ne releve pas la pointe du stylo pour continuer de dessiner.

				// Traitement des Z
				// Modif GBI

				if(this.p1x == this.pPosx && this.p1y == this.pPosy){
					//System.out.println("Go Paper");
					p1z = ONPAPER;
					p2z = ONPAPER;
				}else{
					//System.out.println("Out Paper");
					
					TraitementCoordonnees(pPosx,pPosy,pPosz,pPosx,pPosy,OFFPAPER);
					this.pPosz = OFFPAPER;
					
					TraitementCoordonnees(pPosx,pPosy,pPosz,p1x,p1y,OFFPAPER);
					this.pPosx = this.p1x;
					this.pPosy = this.p1y;
					this.pPosz = this.p1z;
					
					TraitementCoordonnees(pPosx,pPosy,OFFPAPER,p2x,p2y,ONPAPER);
				}
				
				TraitementCoordonnees(p1x,p1y,p1z,p2x,p2y,p2z);
				this.pPosx = this.p2x;
				this.pPosy = this.p2y;
				this.pPosz = this.p2z;
			}else{
				compteurFausseTrame++;
			}
		}
		//System.out.println("Fin TraitementTrame\n");
	}
	//****************************************************************//

	// Envoi des informations sur le Kuka
	public void TraitementCoordonnees(double _p1x,double _p1y,double _p1z,double _p2x, double _p2y, double _p2z){

		MonScriptKuka.GetLine(_p1x, _p1y, _p2x, _p2y, _p1z, _p2z);
		MonScriptKuka.run();
	}

	public void trameStart(){
		
		// Declaration variables
		String AncienneTrame = "";

		// Boucle infinie recuperant les informations dans le socket
		while(Disconect){

			// Recuperation de la trame sur le serveur
			this.Trame = MyServer.getTrame();
			// Si les deux trames sont differentes on effectue le traitement
			if(this.Trame != null){
				//System.out.println("Trame != null\nTrame : " + this.Trame + "\nAncienne Trame : " + AncienneTrame);

				if(this.Trame.equals("DISCONNECTED")){
					System.out.println("DISCONNECTED");
					Disconect = false;
				}else if(!(AncienneTrame.equals(this.Trame))){
					this.TraitementTrame();
					AncienneTrame = this.Trame;
					Trame=null;
				}else{
					compteurTrameIdentique++;
				}
			}
		}
		MonScriptKuka.GetMechanicalZero();
		System.out.println("GetMechanicalZeros");
		System.out.println("Nombre de fausse trame : " + compteurFausseTrame);
		System.out.println("Nombre de trame identique : " + compteurTrameIdentique);
	}

	@Override
	public void run() {
		
	}
}
