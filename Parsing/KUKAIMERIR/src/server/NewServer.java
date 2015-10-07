package server;

import java.io.BufferedReader;
import java.io.InputStreamReader;
import java.io.PrintStream;
import java.net.ServerSocket;
import java.net.Socket;
import Parsing.SocketTrameParsing;


import com.kuka.roboticsAPI.applicationModel.RoboticsAPIApplication;

public class NewServer extends RoboticsAPIApplication {

	int port;
	ServerSocket srv;
	Socket client;
	Socket sock=null;
	PrintStream out=null;
	BufferedReader in=null;
	
	public NewServer() {
		this.port=30002;
		this.client=null;
		this.srv=null;
		//this.run();
	}
	
	public void startProtocol() throws Exception{
		Server tmp = new Server (in,out);
		SocketTrameParsing myParse=new SocketTrameParsing(tmp);
		myParse.trameStart();
	}

	@Override
	public void run() {

		try{
			getLogger().info("Serveur TCP sur le port "+port+"...");

				srv =new ServerSocket(port);
				client=srv.accept();
				startThreadServer(client);
				System.out.println("Connexion entrante sur le port " + port);
		}catch(Exception e){
			getLogger().error("Arret du serveur.");
			System.err.println(e);
		}finally{
			this.close();
		}

	}
	public void startThreadServer(Socket s) {
		System.out.println("Connexion depuis "+client.getInetAddress()+":"+client.getPort()+"...");
		initStreams(client);
	}
	public void initStreams(Socket s)
	{
		try{
			sock=s;
			out=new PrintStream(sock.getOutputStream());
			in=new BufferedReader(new InputStreamReader(sock.getInputStream()));
		}
		catch (Exception e){
			e.printStackTrace();
		}finally{
			try{
				startProtocol();
			}
			catch (Exception e){
				System.err.println(e);
				e.printStackTrace();
			}
			finally{
				close();
			}
		}
	}
	public void close(){
		try{
			getLogger().info("Fermeture de la connexion...");
			if (srv!=null){
				srv.close();
				srv=null;
			}
			if (sock!=null){
                sock.close();
                sock=null;
                out=null;
                in=null;
            }

		}catch (Exception e){
			System.err.println(e);
		}
	}

}
