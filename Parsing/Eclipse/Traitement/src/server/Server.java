package server;

import java.io.BufferedReader;
import java.io.IOException;

public class Server {

	private BufferedReader in=null;
	public Server(BufferedReader reader) {
		this.in=reader;
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

}
