package Parsing;


public class SocketTrameParsing {
	
	private String Trame;
	
	private boolean StopTrame = false;
	
	private double p1x;
	private double p1y;
	private double p2x;
	private double p2y;
	
	public void GetTrame(){
		this.Trame = "LINE:1,21;1:2,02;2,8";
		
	}
	
	public void TraitementTrame(){

		// On récupère l'ordre donné par l'IHM
		// On recherche LINE dans la chaîne récupérée
		int positionOrdreSTOP = this.Trame.indexOf("STOP");
		
		if (positionOrdreSTOP != -1){
			StopTrame = true;
		}
		
		// Si on a pas reçu d'arret de lecture
		if(!StopTrame){
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
				
				// On parse les coordonnées x et y de chaque point
				String[] CoordonneesParts = GeneralParts[1].split(";");
				
				System.out.println("Point X : " + CoordonneesParts[0]);
				
			}
		}		
	}
	
	//************************** CONSTRUCTEUR ************************//
	public SocketTrameParsing(){
		
		System.out.println("Démarrage Construteur ...");
		
		// On crée un courbe proche d'un sinus
				//Vector2 p0 = new Vector2();
				//Vector2 p1 = new Vector2();
				//Vector2 p2 = new Vector2();
				//Vector2 p3 = new Vector2();
		
		// Deux points pour la line
		
		this.p1x = 0.0;
		this.p1y = 0.0;
		this.p2x = 0.0;
		this.p2y = 0.0;
		
		this.Trame = "Constructeur";
		
		System.out.println("Fin Constructeur");
	}
	//***************************************************************//
	
	//**************************** MAIN ****************************//
	public static void main(String[] args) {
		
		SocketTrameParsing MonParsing = new SocketTrameParsing();
		
		MonParsing.GetTrame();
		
		MonParsing.TraitementTrame();
		
	}
	//************************************************************//

}
