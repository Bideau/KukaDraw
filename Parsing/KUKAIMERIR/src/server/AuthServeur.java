package server;
import java.net.Socket;
import java.util.Vector;

import server.Connexion;


public class AuthServeur extends AbstractServeur
{
	
    public AuthServeur() {
//    	super();
    	super(30006);
    	getLogger().info("Constructeur Serveur");
    }
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public void initialize() {
    	getLogger().info("Init Serveur");
    }
    
    public void startThreadServer(Socket s) {
        AuthThread t=new AuthThread(s);
        t.run();
    }
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    /*
    public synchronized void jePars(Connexion cx){
        System.out.println("\n Avant: " + lesPresents.toString());
        if (cx == null)
          return;
        if (lesPresents.contains(cx)){          
          lesPresents.removeElement(cx);
        }
        System.out.println("\n Apres: " + lesPresents.toString());
    }
    
    public synchronized void propage(String ligne)	{
    	//System.out.println(ligne);
    	for (Connexion e : lesPresents){
				e.envoie(ligne);
    	}
    }*/
    
    public void run() {
    	
    public void run() {
    	getLogger().info("RUN Serveur");
    	super.run();
    }
    /*
    public static void main(String[] args){
        new AuthServeur();
        
    }*/
}