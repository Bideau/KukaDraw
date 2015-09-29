package server;

import java.io.BufferedReader;
import java.io.IOException;
import java.net.Socket;

public class Server {

	private BufferedReader in=null;
	private boolean disco=false;
	public Server(BufferedReader reader) {
		this.in=reader;
		disco=false;
	}
	
	public String getTrame(){
		String tmp=null;
		try {
			tmp = in.readLine();
		} catch (IOException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			disco=true;
			
		}
		return tmp;
	}
	
	public boolean isConnected (){
		return disco;
	}

}
