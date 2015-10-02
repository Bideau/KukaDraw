package server;
import java.io.BufferedReader;
import java.io.File;
import java.io.FileReader;
import java.io.PrintStream;
import java.security.MessageDigest;
import java.security.NoSuchAlgorithmException;
import java.net.Socket;

public class AuthThread extends AbstractAuthThread
{
	private String fichier="users.txt";
	
	public AuthThread(Socket client) {
		super(client);
		getLogger().info("Constructeur Thread");
	}

	 /*
	// Verification de l'utilisateur a partir du fichier User.txt
	public boolean verifUser(String user){
		boolean verif =true;
		String ligne;
		String [] tampon;
		BufferedReader ficTexte ;
		System.out.println(user);
		try{
			ficTexte = new BufferedReader(new FileReader(new File(fichier)));
			do {
				ligne = ficTexte.readLine();
				if (ligne != null) {
					tampon= ligne.split(":");
					if (tampon[0].equals(user)){
						return true;
					}
				}else{
					if(ficTexte.read()==-1){
						verif=false;
					}
				}

			} while (verif);
		}		
		catch (Exception e){
			System.out.println(e.toString());
		}
		if(user.equals("toto")){
			return true;
		}
		return false;
		return true;
	}

	public boolean verifPass(String user,String pass){
		boolean verif =true;
		String ligne;
		String passSHA1;
		String [] tampon;
		BufferedReader ficTexte ;
		try{
			ficTexte = new BufferedReader(new FileReader(new File(fichier)));
			do {
				ligne = ficTexte.readLine();
				if (ligne != null) {
					tampon= ligne.split(":");
					passSHA1 = sha1(tampon[1]);
					if (tampon[0].equals(user) && tampon[1].equals(passSHA1)){
						return true;
					}else if (tampon[0].equals(user)){
						System.out.println("Mot de passe invalide");
						verif = false;
					}
				}
				if(ficTexte.read()==-1){
					verif=false;
				}
			} while (verif);
		}		
		catch (Exception e){
			System.out.println(e.toString());
		}
		if(pass.equals("titi")){
			return true;
		}
		return false;
		return true;
	}

	public static String sha1(String input) throws NoSuchAlgorithmException {
		MessageDigest mDigest = MessageDigest.getInstance("SHA1");
		byte[] result = mDigest.digest(input.getBytes());
		StringBuffer sb = new StringBuffer();
		for (int i = 0; i < result.length; i++) {
			sb.append(Integer.toString((result[i] & 0xff) + 0x100, 16).substring(1));
		}

		return sb.toString();
	}*/
}