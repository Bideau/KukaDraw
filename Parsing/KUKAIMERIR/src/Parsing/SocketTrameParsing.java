package Parsing;

import server.Server;
import application.ScriptKuka;
public class SocketTrameParsing {

	private String Trame;

	private boolean StopTrame = false;

	private double p1x;
	private double p1y;
	private double p2x;
	private double p2y;

	private Server MyServer;
	private ScriptKuka MonScriptKuka;

	//************************** CONSTRUCTEUR ************************//
	public SocketTrameParsing(Server ServerParametre){

		System.out.println("Démarrage Construteur ...");

		this.MyServer = ServerParametre;
		this.MonScriptKuka = new ScriptKuka();

		// Initialisation des coordonnées des différents points
		this.p1x = 0.0;
		this.p1y = 0.0;
		this.p2x = 0.0;
		this.p2y = 0.0;

		// Initialisation de la trame de comunication
		this.Trame = "Default";

		// Initialisation du booléen permettatn de stopper la lecture dans la socket
		this.StopTrame = false;

		System.out.println("Fin Constructeur\n");
	}
	//***************************************************************//

	//************************ TRAITEMENT TRAME *********************//
	public void TraitementTrame(){
		System.out.println("Démarrage TraitementTrame ...");

		// On récupère l'ordre donné par l'IHM
		// On recherche LINE dans la chaîne récupérée
		int positionOrdreSTOP = this.Trame.indexOf("STOP");
		int positionOrdreSTART = this.Trame.indexOf("START");

		if (positionOrdreSTOP != -1){
			System.out.println("Reçu STOP");
			StopTrame = true;
		}

		if (positionOrdreSTART != -1){
			System.out.println("Reçu START");
			this.MonScriptKuka.ApprochePaper();
			StopTrame = false;
		}

		// Si on a reçu un arret de lecture
		if(StopTrame){
			System.out.println("STOP !");
		}else{
			// On récupère l'ordre donné par l'IHM
			// On recherche LINE dans la chaîne récupérée
			int positionOrdreLINE = this.Trame.indexOf("LINE");

			// Si on trouve LINE dans la chaîne
			if(positionOrdreLINE != -1){

				// Chaîne LINE trouvé !
				// On recherche les coordonnées des deux points
				String[] GeneralParts = this.Trame.split(":");

				for(int i=0; i<GeneralParts.length; i++){
					System.out.println("parts " + i + " : " + GeneralParts[i]);
				}

				// On parse les coordonnées x et y du 1er point
				String[] CoordonneesPoint1 = GeneralParts[1].split(";");

				this.p1x = Double.parseDouble(CoordonneesPoint1[0]);
				this.p1y = Double.parseDouble(CoordonneesPoint1[1]);

				System.out.println("\nPoint 1 :");
				System.out.println("Coordonnée X : string(" + CoordonneesPoint1[0] + ") / double(" + this.p1x + ")");
				System.out.println("Coordonnée Y : string(" + CoordonneesPoint1[1] + ") / double(" + this.p1y + ")");

				// On parse les coordonnées x et y du 2eme point
				String[] CoordonneesPoint2 = GeneralParts[2].split(";");

				this.p2x = Double.parseDouble(CoordonneesPoint2[0]);
				this.p2y = Double.parseDouble(CoordonneesPoint2[1]);

				System.out.println("\nPoint 2 :");
				System.out.println("Coordonnée X : string(" + CoordonneesPoint2[0] + ") / double(" + this.p2x + ")");
				System.out.println("Coordonnée Y : string(" + CoordonneesPoint2[1] + ") / double(" + this.p2y + ")");
			
				MonScriptKuka.GetLine(this.p1x, this.p1y, this.p2x, this.p2y);
				MonScriptKuka.runApplication();
			}
		}
		System.out.println("Fin TraitementTrame\n");
	}
	//****************************************************************//

	public void trameStart(){
		
		// Déclaration variables
		//int i = 0;
		String AncienneTrame = "";

		// Boucle infinie récupérant les informations dans le socket
		while(MyServer.isConnected()){
			System.out.println("Boucle infinie");
			
			// Récupération de la trame sur le serveur
			this.Trame = MyServer.getTrame();
			
			// Temporisation de 1s
			try {
				Thread.sleep(500);
			} catch (InterruptedException e) {
				// TODO Auto-generated catch block
				e.printStackTrace();
			}

			// Si les deux trames sont différentes on effectue le traitement
			if(this.Trame != null){
				System.out.println("Trame != null\nTrame : " + this.Trame + "\nAncienne Trame : " + AncienneTrame);
				if(!(AncienneTrame.equals(this.Trame))){
					//System.out.println("toto 3");
					//MonParsing.Trame = "LINE:1.21;1:2.02;2.8";
					//System.out.println(this.Trame);
					this.TraitementTrame();
					AncienneTrame = this.Trame;
				}
			}
		}
	}
}
