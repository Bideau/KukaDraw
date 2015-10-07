package textParsing;
import java.awt.Font;
import java.awt.Shape;
import java.awt.font.FontRenderContext;
import java.awt.font.GlyphVector;
import java.awt.geom.PathIterator;
import java.text.DecimalFormat;
import java.text.DecimalFormatSymbols;
import java.util.Locale;

public class TextParse {
	private boolean lineOn;
	private boolean quadOn;
	private boolean cubicOn;
	private String s;
	private String Result;
	private DecimalFormat df;
	DecimalFormatSymbols otherSymbols;

	public TextParse() {
		lineOn=false;
		quadOn=false;
		cubicOn=false;
		Result ="";
		s="toto";
		otherSymbols = new DecimalFormatSymbols(Locale.FRANCE);
		otherSymbols.setDecimalSeparator(',');
		df = new DecimalFormat("#.#####",otherSymbols);

	}


	private void describeCurrentSegment(PathIterator pi) {
		double[] coordinates = new double[6];
		int type = pi.currentSegment(coordinates);
		switch (type) {
		case PathIterator.SEG_MOVETO:
			//System.out.println("move to " + coordinates[0] + ", " + coordinates[1]);
			lineOn=false;
			quadOn=false;
			cubicOn=false;
			Result=Result+"m "+df.format(coordinates[0])+" "+df.format(coordinates[1]);
			break;

		case PathIterator.SEG_LINETO:
			//System.out.println("line to " + coordinates[0] + ", " + coordinates[1]);
			if(lineOn){

			}else{
				Result=Result+" l";
				lineOn=true;
				quadOn=false;
				cubicOn=false;
			}
			Result=Result+" "+df.format(coordinates[0])+" "+df.format(coordinates[1]);
			break;

		case PathIterator.SEG_QUADTO:
			//System.out.println("quadratic to " + coordinates[0] + ", " + coordinates[1] + ", "
			//		+ coordinates[2] + ", " + coordinates[3]);
			if(quadOn){
				Result=Result+" c";
			}else{
				lineOn=false;
				quadOn=true;
				cubicOn=false;
			}
			Result=Result+" "+df.format(coordinates[0])+" "+df.format(coordinates[1])+" "+df.format(coordinates[2])+" "+df.format(coordinates[3]);
			break;

		case PathIterator.SEG_CUBICTO:
			//System.out.println("cubic to " + coordinates[0] + ", " + coordinates[1] + ", "
			//		+ coordinates[2] + ", " + coordinates[3] + ", " + coordinates[4] + ", " + coordinates[5]);
			if(cubicOn){
				Result=Result+" c";
			}else{
				lineOn=false;
				quadOn=false;
				cubicOn=true;
			}
			Result=Result+" "+df.format(coordinates[0])+" "+df.format(coordinates[1])+" "+df.format(coordinates[2])+" "+df.format(coordinates[3])
					+" "+df.format(coordinates[4])+" "+df.format(coordinates[5]);
			break;

		case PathIterator.SEG_CLOSE:
			//System.out.println("close");
			lineOn=false;
			quadOn=false;
			cubicOn=false;
			break;

		default:
			lineOn=false;
			quadOn=false;
			cubicOn=false;
			break;
		}

	}
	public String GetPath(String TestString,String FontName,String FontType,int FontSize){
		int style=0;
		if (FontType.equals("ITALIC")){
			style=1;
		}else if (FontType.equals("BOLD")){
			style=2;
		}
		Font font = new Font(FontName, style, FontSize);
		GlyphVector gv = font.createGlyphVector(new FontRenderContext(null,false,false), this.s);
		Shape sh = gv.getOutline();
		PathIterator pi = sh.getPathIterator(null);

		while (pi.isDone() == false) {
			this.describeCurrentSegment(pi);
			pi.next();
		}
		return Result;
	}

	public static void main(String[] args) throws Exception {

		String TestString="Test";
		String FontName="Courier";
		String FontType="PLAIN";
		int FontSize=12;
		boolean go=true;
		if(args.length == 1){
			TestString=args[0];
		}else if(args.length == 2 ){
			TestString=args[0];
			FontName=args[1];
		}else if(args.length == 3 ){
			TestString=args[0];
			FontName=args[1];
			FontType=args[2];
		}else if(args.length == 4 ){
			TestString=args[0];
			FontName=args[1];
			FontType=args[2];
			FontSize=Integer.parseInt(args[3]);
		}else {
			System.out.println("Probleme dans les arguments");
			System.out.println("Args : DrawString FontName FontType FontSize");
			go=false;
		}
		if (go){
			TextParse tmp = new TextParse();
			//System.out.println(tmp.GetPath("t","Courier", "PLAIN", 12));
			System.out.println(tmp.GetPath(TestString,FontName, FontType, FontSize));
		}
	}

}


