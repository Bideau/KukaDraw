package server;
import java.io.IOException;
import java.net.Socket;
public abstract class AbstractAuthThread extends AbstractThreadText
{
	AbstractAuthThread(Socket client) {
		super(client);
	}
	//	public abstract boolean verifUser(String user);
	//	public abstract boolean verifPass(String user,String pass);
	///////////////////////////////////////////////
	///////////////////////////////////////////////
	public void startProtocol() throws Exception
	{
		// User authentification
		/*
		out.println("USER:"); // --> vers le client !
		String user=in.readLine(); // <-- depuis le client !
		System.out.println("User : "+user);
		// if (user.equals("toto"))
		if (verifUser(user))
		{
			out.println("PASS:"); // --> vers le client !
			String pass=in.readLine(); // <-- depuis le client !
			System.out.println("Pass : "+pass);
			// if (pass.equals("titi"))
			if (verifPass(user,pass))
			{
				out.println("OK"); // --> vers le client !
			}
		}*/
		
		SocketTrameParsing myParse=new SocketTrameParsing(new Server(in));
		(new Thread(myParse)).start();
	}
}