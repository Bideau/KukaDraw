package server;
import java.io.*;
import java.net.*;

import server.AbstractServeur;
//import server.AuthServeur;
 

class Connexion implements Runnable{
	protected BufferedReader in = null;
	protected PrintStream out = null;
	protected Socket client = null;
	protected AuthServeur serveur = null;

	public Connexion(AbstractServeur abstractServeur, Socket cli){
		client=cli;	
		try{
			in = new BufferedReader(new InputStreamReader(client.getInputStream()));
			out = new PrintStream(client.getOutputStream());
		} // fin try
		catch (IOException e0){
			try{
				client.close();
			}
			catch (IOException e1) {}
			System.err.println(e0.getMessage());
			return;
		} // fin catch
		serveur = (AuthServeur) abstractServeur;
	}


	public void run(){
		String ligne;
		try {
			while(true){
				ligne = in.readLine();
				//serveur.propage(ligne);
				if((ligne == null)||(ligne.equals("fin"))){
					break;
				}
			}
		} // fin try
		catch (IOException e){
			System.out.println("Pb Connexion: " + e.toString());
		} // fin catch
		
		finally{
			try{
				if (client!= null){
					in.close();
					out.close();
					client.close();
					in = null;
					out = null;
					client = null;
					System.out.println("Fermeture connexion");
				}
			} catch (IOException e) {}
			//serveur.jePars(this);
		} // fin finally
	}

	public void envoie(String ligne){
		System.out.println(ligne);
		/*if(client!=null){
			if(out!=null){
				out.println(ligne);
			}
		}*/
	}
}