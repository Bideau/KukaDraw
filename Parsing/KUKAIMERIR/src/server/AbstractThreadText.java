package server;
/*********************************************
* Un squelette de client TCP.
**********************************************/
import java.io.PrintStream;
import java.io.InputStreamReader;
import java.io.BufferedReader;
import java.net.Socket;

import com.kuka.roboticsAPI.applicationModel.RoboticsAPIApplication;
public abstract class AbstractThreadText extends RoboticsAPIApplication
{
    Socket sock=null;
    PrintStream out=null;
    BufferedReader in=null;
    ///////////////////////////////////////////////
    // Pour un client
    ///////////////////////////////////////////////
    public AbstractThreadText(String host,int port)
    {
        try
        {
        	getLogger().info("Connexion vers "+host+":"+port+"...");
            initStreams(new Socket(host,port));
        }
        catch (Exception e)
        {
            System.err.println(e);
        }
    }
    
    ///////////////////////////////////////////////
    // Pour un serveur
    ///////////////////////////////////////////////
    public AbstractThreadText(Socket client)
    {
    	System.out.println("Connexion depuis "+client.getInetAddress()+":"+client.getPort()+"...");
        initStreams(client);
        
    }
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public void initStreams(Socket s)
    {
        try
        {
            sock=s;
            out=new PrintStream(sock.getOutputStream());
            in=new BufferedReader(new InputStreamReader(sock.getInputStream()));
        }
        catch (Exception e)
        {
            //System.err.println(e);
            e.printStackTrace();
        }finally {
        	this.run();
        }
    }
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public void close()
    {
        try
        {
        	getLogger().info("Fermeture de la connexion...");
            if (sock!=null)
            {
                sock.close();
                sock=null;
                out=null;
                in=null;
            }
        }
        catch (Exception e)
        {
            System.err.println(e);
        }
    }
    

    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public abstract void startProtocol() throws Exception;
    ///////////////////////////////////////////////
    ///////////////////////////////////////////////
    public void run()
    {
        try
        {
            startProtocol();
        }
        catch (Exception e)
        {
            System.err.println(e);
            e.printStackTrace();
        }
        finally
        {
           close();
        }
    }
}