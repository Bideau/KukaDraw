package server;

import java.io.BufferedReader;
import java.io.IOException;
import java.net.Socket;

public class Server {

	private BufferedReader in=null;
	private Socket client=null;
	public Server(BufferedReader reader,Socket sock) {
		this.in=reader;
		this.client=sock;
	}
	
	public String getTrame(){
		String tmp=null;
		try {
			tmp = in.readLine();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
		}
		return tmp;
	}
	
	public boolean isConnected (){
		if (client == null){
			return false;
		}
		return true;
	}

}
