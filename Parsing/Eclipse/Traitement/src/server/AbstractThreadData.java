package server;
/*********************************************
* Un squelette de client TCP.
**********************************************/
import java.io.DataOutputStream;
import java.io.DataInputStream;
import java.net.Socket;
public abstract class AbstractThreadData extends Thread
{
  Socket sock=null;
  DataOutputStream out=null;
  DataInputStream in=null;
  ///////////////////////////////////////////////
  // Pour un client
  ///////////////////////////////////////////////
  AbstractThreadData(String host,int port)
  {
    try
    {
      System.out.println("Connexion vers "+host+":"+port+"...");
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
  AbstractThreadData(Socket client)
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
      out=new DataOutputStream(sock.getOutputStream());
      in=new DataInputStream(sock.getInputStream());
    }
    
    catch (Exception e)
    {
      //System.err.println(e);
      e.printStackTrace();
    }
  }
  ///////////////////////////////////////////////
  ///////////////////////////////////////////////
  public void close()
  {
    try
    {
      System.out.println("Fermeture de la connexion...");
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
      //System.err.println(e);
      e.printStackTrace();
    }
    finally
    {
      close();
    }
  }
}