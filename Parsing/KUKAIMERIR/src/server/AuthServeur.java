package server;
import java.net.Socket;
import java.util.Vector;

import server.Connexion;


public class AuthServeur extends AbstractServeur
{
	
    AuthServeur() {
    	super(30002);
    }
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public void startThreadServer(Socket s) {
        AuthThread t=new AuthThread(s);
        t.start();
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
    	
    }
    /*
    public static void main(String[] args){
        new AuthServeur();
        
    }*/
}