using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;


namespace regex01
{
    class Program
    {
        static void Main(string[] args)
        {
            var html = "";

            using (WebClient web = new WebClient())
            {
                Encoding iso = Encoding.GetEncoding("ISO-8859-1");
                web.Encoding = iso;
                html = web.DownloadString("http://esaj.tjrn.jus.br/cpo/pg/show.do?processo.codigo=2Y000370T0000&processo.foro=106");
            }


            Regex regex_processo = new Regex(@"\d{7}-?\d{2}.\d{4}.\d{1}.\d{2}.{5}", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_processo = regex_processo.Matches(html);
            //n° do processo

            //--------------------------------
            Regex regex_status = new Regex("<span style=\"color: red;\\s*\"\\s*>(.*?)</span>\\s*</td>\\s*</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_status = regex_status.Matches(html);//grupo $2

            //status

            //--------------------------------
            Regex classe = new Regex("<span id=\"\">(.*?)</span>\\s*</span>\\s*</td>\\s*</tr>\\s*</table>", RegexOptions.CultureInvariant | RegexOptions.IgnoreCase);
            var match_classe = classe.Matches(html);//grupo $2

            //classe

            //--------------------------------
            Regex regex_area = new Regex("<span class=\"labelClass\">Área:</span> (.*?) \\s*</td>\\s*</tr>\\s*</table>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_area = regex_area.Matches(html);//grupo $4
            //area

            //--------------------------------
            Regex regexAssunto = new Regex("<td valign=\"\">\\s*<span id=\"\">(.*?)</span>\\s*</td>\\s*</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_assunto = regexAssunto.Matches(html);//grupo $3
            //assunto

            //--------------------------------
            Regex distribuicao = new Regex("<td valign=\"\">\\s*<span id=\"\">(.*?)</span>\\s*</td>\\s*</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_distribuicao = distribuicao.Matches(html);//grupo $3

            //distribuição

            Console.WriteLine("*************DADOS DO PROCESSO****************");
            Console.WriteLine(" ");

            if (match_processo.Count > 0)
            {
                Console.WriteLine("Processo : " + "   " + match_processo[0].Value);
                //Console.WriteLine("itens encotrados : \n" + +match.Count/2);
            }

            //n° processo

            if (match_status.Count > 0)
            {
                Console.WriteLine("Status :" + "   " + match_status[0].Groups[1].Value);
            }

            //status

            if (match_classe.Count > 0)
            {
                //Console.WriteLine("classe :" + match_classe[0].Value);
                Console.WriteLine("classe :" + "   " + match_classe[0].Groups[1].Value);
            }

            //classe

            if (match_area.Count > 0)
            {
                //Console.WriteLine("area : " + match_area[0].Value);
                Console.WriteLine("Area :" + "   " + match_area[0].Groups[1].Value);
            }

            //area

            if (match_assunto.Count > 0)
            {
                //Console.WriteLine("assunto" + match_assunto[0].Value);
                Console.WriteLine("assunto :" + "   " + match_assunto[0].Groups[1].Value);
            }
            //assunto


            if (match_distribuicao.Count > 0)
            {
                //Console.WriteLine("Distribuição :" + match_distribuicao[0].Value);
                Console.WriteLine("distribuição:" + "   " + match_distribuicao[1].Groups[1].Value + "\n" + match_distribuicao[2].Groups[1].Value);
            }

            //Distribuição

            // Fim dados do processo
            Console.WriteLine(" ");
            Console.WriteLine("*********PARTES DO PROCESSO******************");
            Console.WriteLine(" ");

            /*Regex partes_do_processo = new Regex("\\s*<span class=\"mensagemExibindo\">(.*?)&nbsp;</span>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_p01 = partes_do_processo.Matches(html);
            // Partes do processo

            Regex entidades_do_processo = new Regex("\"\\*\" align=\"left\"\\s*style=\"padding-bottom: 5px\">\\s*(.*?)\\s*</td>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var match_entidades = entidades_do_processo.Matches(html);*/

            //*********************************************************

            Regex rgxProcesso = new Regex("<td\\s*valign=\"top\"\\s*align=\"right\"\\s*width=\"141\"\\s*style=\"padding-bottom:\\s*5px\">\\s*<span\\s*class=\"mensagemExibindo\">(.*?)&nbsp;</span>\\s*</td>\\s*<td width=\"\\*\"\\s*align=\"left\"\\s*style=\"padding-bottom:\\s*5px\">\\s*(.*?)\\s*</td>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var matchRgxProcesso = rgxProcesso.Matches(html);

            if (matchRgxProcesso.Count > 0)
            {
                Console.WriteLine(matchRgxProcesso[0].Groups[1].Value + matchRgxProcesso[0].Groups[2].Value + "\n" + matchRgxProcesso[1].Groups[1].Value + matchRgxProcesso[1].Groups[2].Value);
            }

            // Fim das partes do processo
            Console.WriteLine(" ");
            Console.WriteLine("**********MOVIMENTAÇÕES*******************");
            Console.WriteLine(" ");
            // Start movimentação

            /*Regex rgx_datas = new Regex("<tr\\s*class=\"fundoEscuro\"\\s*style=\"\">\\s*<td\\s*width=\"120\"\\s*style=\"vertical-align:\\s*top\">\\s*(.*?)\\s*</td>\\s*<td width=\"20\"\\s*valign=\"top\">\\s*<a\\s*class=\"linkMovVincProc\"\\s*title=\"Visualizar documento em inteiro teor\"\\s*href=\"\\#\"\\s*name=\"M\"\\s*cdDocumento=.>\\s*<img\\s*src=\"/cpo/imagens/doc2.gif\"\\s*width=\"16\"\\s*height=\"16\"\\s*border=\"0\"/>\\s*</a>\\s*</td>\\s*<td\\s*style=\"vertical-align:\\s*top;\\s*padding-bottom:\\s*5px\"\\s*>\\s*<a\\s*class=\"linkMovVincProc\"\\s*title=\"Visualizar documento em inteiro teor\"\\s*href=\"\\#\"\\s*name=\"M\"\\s*cdDocumento=(.*?)>\\s*\\s*</a>\\s*<br/>\\s*<span style=\"font-style:\\s*italic;\">(.*?)</span>\\s*</td>\\s*</tr>\\s*<tr\\s*class=\"fundoClaro\"\\s*style=\"\">\\s*<td\\s*width=\"120\"\\s*style=\"vertical-align:\\s*top\">\\s*(.*?)\\s*</td>\\s*<td\\s*width=\"20\"\\s*valign=\"top\">\\s*</td>\\s*<td\\s*style=\"vertical-align:\\s*top;\\s*padding-bottom:\\s*5px\"\\s*>\\s*(.*?)\\s*<br/>\\s*<span style=\"font-style:\\s*italic;\"></span>\\s*</td>\\s*</tr>\\s*</table>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var matchDatas = rgx_datas.Matches(html);

            Regex rgxdatas2 = new Regex("<tr class=\"fundoClaro\" style=\"\">\\s*<td width=\"120\" style=\"vertical-align: top\">\\s*(.*?)\\s*</td>\\s*<td width=\"20\" valign=\"top\">\\s*<a class=\"linkMovVincProc\" title=\"Visualizar documento em inteiro teor\" href=\"\\#\" name=\"M\" cdDocumento=\"36817960\">\\s*<img src=\"/cpo/imagens/doc2.gif\" width=\"16\" height=\"16\" border=\"0\"/>\\s*</a>\\s*</td>\\s*<td style=\"vertical-align: top; padding-bottom: 5px\" >\\s*<a class=\"linkMovVincProc\" title=\"Visualizar documento em inteiro teor\" href=\"\\#\" name=\"M\" cdDocumento=\"36817960\">\\s*Homologada a Transação\\s*</a>\\s*<br/>\\s*<span style=\"font-style: italic;\">(.*?)</span>\\s*</td>\\s*</tr>", RegexOptions.Singleline | RegexOptions.IgnoreCase);
            var matchdata2 = rgxdatas2.Matches(html);*/

            Regex regexMovimentacao = new Regex("<tr\\s*class=\"fundoClaro\"\\s*style=\"\">\\s*<td\\s*width=\"120\"\\s*style=\"vertical-align:\\s*top\">\\s*(.*?)\\s*</td>\\s*<td\\s*width=\"20\"\\s*valign=\"top\">\\s*<a\\s*class=\"linkMovVincProc\"\\s*title=\"Visualizar documento em inteiro teor\"\\s*href=\"\\#\"\\s*name=\"M\"\\s*cdDocumento=\"36817960\">\\s*<img\\s*src=\"/cpo/imagens/doc2.gif\"\\s*width=\"16\"\\s*height=\"16\"\\s*border=\"0\"/>\\s*</a>\\s*</td>\\s*<td style=\"vertical-align:\\s*top;\\s*padding-bottom:\\s*5px\"\\s*>\\s*<a\\s*class=\"linkMovVincProc\"\\s*title=\"Visualizar documento em inteiro teor\"\\s*href=\"\\#\"\\s*name=\"M\"\\s*cdDocumento=\"36817960\">\\s*(.*?)\\s*</a>\\s*<br/>\\s*<span\\s*style=\"font-style:\\s*italic;\">(.*?)</span>\\s*</td>\\s*</tr>\\s*<tr\\s*class=\"fundoEscuro\"\\s*style=\"\">\\s*<td\\s*width=\"120\"\\s*style=\"vertical-align:\\s*top\">\\s*(.*?)\\s*</td>\\s*<td\\s*width=\"20\"\\s*valign=\"top\">\\s*<a\\s*class=\"linkMovVincProc\"\\s*title=\"Visualizar documento em inteiro teor\"\\s*href=\"\\#\"\\s*name=\"M\"\\s*cdDocumento=(.*?)>\\s*<img src=\"/cpo/imagens/doc2.gif\"\\s*width=\"16\"\\s*height=\"16\"\\s*border=\"0\"/>\\s*</a>\\s*</td>\\s*<td\\s*style=\"vertical-align:\\s*top;\\s*padding-bottom:\\s*5px\"\\s*>\\s*<a\\s*class=\"linkMovVincProc\"\\s*title=\"Visualizar documento em inteiro teor\"\\s*href=\"\\#\"\\s*name=\"M\"\\s*cdDocumento=(.*?)>\\s*(.*?)\\s*</a>\\s*<br/>\\s*<span\\s*style=\"font-style:\\s*italic;\">(.*?)</span>\\s*</td>\\s*</tr>\\s*<tr\\s*class=\"fundoClaro\"\\s*style=\"\">\\s*<td\\s*width=\"120\"\\s*style=\"vertical-align:\\s*top\">\\s*(.*?)\\s*</td>\\s*<td\\s*width=\"20\"\\s*valign=\"top\">\\s*</td>\\s*<td\\s*style=\"vertical-align:\\s*top;\\s*padding-bottom:\\s*5px\"\\s*>\\s*(.*?)\\s*<br/>\\s*<span\\s*style=\"font-style:\\s*italic;\"></span>\\s*</td>\\s*</tr>\\s*</table>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var matchMovimentacao = regexMovimentacao.Matches(html);

            if (matchMovimentacao.Count > 0)
            {
                Console.WriteLine(matchMovimentacao[0].Groups[1].Value + " " +matchMovimentacao[0].Groups[2].Value+"\n"+matchMovimentacao[0].Groups[3].Value);

                Console.WriteLine(" ");

                Console.WriteLine(matchMovimentacao[0].Groups[4].Value+" "+matchMovimentacao[0].Groups[7].Value+"\n"+ matchMovimentacao[0].Groups[8].Value);

                Console.WriteLine(" ");

                Console.WriteLine(matchMovimentacao[0].Groups[9].Value + " " +matchMovimentacao[0].Groups[10].Value);
            }
            Console.WriteLine(" ");
            Console.WriteLine("*****************************");
            Console.WriteLine(" ");

            // end movimentação

            Regex regextp = new Regex("<tr>\\s*<td.*?>\\s*(.*?)\\s*</td>\\s*</tr>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var matchRgxtp = regextp.Matches(html);

            //<table\\s*width=\"100\\s*%\\s*\"\\s*border=\"0\"\\s*cellspacing=\"0\"\\s*cellpadding=\"0\">\\s*<tr\\s*valign=\"top\">\\s*<td\\s*height=\"21\"\\s*valign=\"top\"\\s*nowrap\\s*background=\"\\s*/\\s*cpo\\s*/\\s*imagens\\s*/\\s*spw\\s*/\\s*fundo_subtitulo.gif\"\\s*class=\"subtitle\">\\s*(.*?)\\s*</td>\\s*<td\\s*background=\"\\s*/\\s*cpo\\s*/\\s*imagens\\s*/\\s*spw\\s*/\\s*fundo_subtitulo2.gif\"\\s*width=\"90\\s*%\\s*\">\\s*<img\\s*src=\"\\s*/\\s*cpo\\s*/\\s*imagens\\s*/\\s*spw\\s*/\\s*final_subtitulo.gif\"\\s*width=\"16\"\\s*height=\"20\"></td>\\s*</tr>\\s*</table>\\s*</div>\\s*<table\\s*style=\"margin-left:15px;\\s*margin-top:1px;\\s*\"\\s*align=\"center\"\\s*width=\"98\\s*%\\s*\"\\s*border=\"0\"\\s*cellspacing=\"0\"\\s*cellpadding=\"1\"\\s*>\\s*<tr>\\s*<td\\s*colspan=\"3\"\\s*align=\"left\">\\s*(.*?)\\s*</td>\\s*</tr>
            //<tr\\s*valign=\"top\">\\s*<td\\s*height=\"21\"\\s*valign=\"top\"\\s*nowrap\\s*background=\"\\s*/\\s*cpo\\s*/\\s*imagens\\s*/\\s*spw\\s*/\\s*fundo_subtitulo.gif\"\\s*class=\"subtitle\">\\s*(.*?)\\s*</td>\\s*<td\\s*background=\"/\\s*cpo\\s*/\\s*imagens\\s*/\\s*spw\\s*/\\s*fundo_subtitulo2.gif\"\\s*width=\"90\\s*%\\s*\">\\s*<img\\s*src=\"\\s*/\\s*cpo\\s*/\\s*imagens\\s*/\\s*spw\\s*/\\s*final_subtitulo.gif\"\\s*width=\"16\"\\s*height=\"20\"></td>\\s*</tr>\\s*</table>\\s*</div>\\s*<table\\s*style=\"margin-left:15px;\\s*margin-top:1px;\\s*\"\\s*align=\"center\"\\s*width=\"98\\s*%\\s*\"\\s*border=\"0\"\\s*cellspacing=\"0\"\\s*cellpadding=\"1\">\\s*<tr>\\s*<td\\s*colspan=\"3\"\\s*align=\"left\"\\s*>\\s*(.*?)\\s*</td>\\s*</tr>
            if (matchRgxtp.Count > 0)
            {
                Console.WriteLine("Incidentes, ações incidentais, recursos e execuções de sentenças :"+"\n"+ matchRgxtp[20].Groups[1].Value);
                Console.WriteLine(" ");

                Console.WriteLine("Petições diversas :"+"\n" +  matchRgxtp[21].Groups[1].Value);
                Console.WriteLine(" ");

                Console.WriteLine("Audiências :"+"\n" + matchRgxtp[22].Groups[1].Value);

            }


            Console.ReadKey();
        }
    }
}
