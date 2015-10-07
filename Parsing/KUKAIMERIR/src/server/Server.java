package server;

import java.io.BufferedReader;
import java.io.IOException;
import java.io.PrintStream;

public class Server {

	private BufferedReader in=null;
	private PrintStream out=null;
	private boolean disco=true;
	public Server(BufferedReader reader, PrintStream writer) {
		this.in=reader;
		this.out=writer;
		disco=true;
	}

	public String getTrame(){
		String tmp=null;
		try {
			if (in != null){
				tmp = in.readLine();
				if (tmp !=null){
					if (tmp.equals("DISCONNECTED")){
						System.out.println("Test");
						disco=false;
						
					}
				}
			}

		} catch (IOException e) {
			e.printStackTrace();
			disco=true;
		}
		return tmp;
	}
	
	public void log(String message){
		out.println(message);
	}

	public boolean isConnected (){
		return disco;
	}

}
