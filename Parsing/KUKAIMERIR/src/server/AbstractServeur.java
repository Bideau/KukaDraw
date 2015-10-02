package server;
/*********************************************
* Un squelette de serveur TCP Multi-Processus.
**********************************************/
import java.io.BufferedReader;
import java.io.IOException;
import java.io.InputStreamReader;
import java.net.Socket;
import java.net.ServerSocket;
import java.util.Vector;

import com.kuka.roboticsAPI.applicationModel.RoboticsAPIApplication;

import server.Connexion;


public abstract class AbstractServeur extends RoboticsAPIApplication
{
    int port;
    ServerSocket srv;
    Socket client;
    protected Vector <Connexion>lesPresents;
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public AbstractServeur(){
    	this(30002);
    }
    public AbstractServeur(int port)
    {
    	getLogger().info("Constructeur Serveur Abstract");
    	this.port=port;
        this.client=null;
        this.srv=null;
        //this.run();
        lesPresents=new Vector<Connexion>();
    }
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public abstract void startThreadServer(Socket s);
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public void initialize() {
    	getLogger().info("Init Serveur Abstract");
    }
    public void run()    {
    	getLogger().info("Run Serveur Abstract");
        try
        {
        	getLogger().info("Serveur TCP sur le port "+port+"...");
            srv =new ServerSocket(port);
            //srv.setSoTimeout(10000);
            while(true){
                client=srv.accept();
                startThreadServer(client);
                //Prob
                lesPresents.addElement(new Connexion(this, srv.accept()));
        		(new Thread((Connexion)lesPresents.lastElement())).start();
        		getLogger().info("Connexion entrante sur le port " + port);
                
           }
        }
        catch(Exception e)
        {
        	getLogger().error("Arret du serveur.");
            System.err.println(e);
        }
    }
    /*
    public void ecoute() {
		try{
			while(true){
        		lesPresents.addElement(new Connexion(this, srv.accept()));
        		(new Thread(lesPresents.lastElement())).start();
			      System.out.println("Connexion entrante sur le port " + port);
        	}
        } // fin try
        catch (IOException e){
            System.err.println(e.getMessage());
            System.exit(1);
        } // fin catch
	}*/
   
}