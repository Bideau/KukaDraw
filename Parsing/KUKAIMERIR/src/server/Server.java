package server;

import java.io.BufferedReader;
import java.io.IOException;
import java.net.Socket;

public class Server {

	private BufferedReader in=null;
	private boolean disco=true;
	public Server(BufferedReader reader) {
		this.in=reader;
		disco=true;
	}

	public String getTrame(){
		String tmp=null;
		try {
			tmp = in.readLine();
			if (tmp !=null){
				if (tmp.equals("DISCONNECTED")){
					System.out.println("Test");
					disco=false;
				}
			}

		} catch (IOException e) {
			e.printStackTrace();
			disco=true;
		}
		return tmp;
	}

	public boolean isConnected (){
		return disco;
	}

}
