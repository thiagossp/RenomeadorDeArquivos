# Motivação
A motivação para desenvolver o software surgiu da necessidade do setor de concursos públicos da empresa ter que disponibilizar uma cópia digital do gabarito para o candidato por meio do portal. 
Para isso, seria necessário que os arquivos digitalizados fossem renomeados com base no número de inscrição do candidato, como forma de identificar o arquivo correto. Porém, apesar do sistema de digitalização extrair o número da inscrição das imagens e grava-lo em um arquivo .xls, ele não permitia salvar os arquivos diretamente com essa informação.

# Objetivos
+ Renomear arquivos de imagem (*.jpg* e *.TIF*) automaticamente, utilizando como referência uma planilha em Excel (*.xls* e *.xlsx*) onde deve conter, ao menos, uma coluna com os nomes atuais dos arquivos e uma coluna contendo os novos nomes.
+ Criptografar o nome do arquivo utilizando *SHA1* objetivando preservar o sigilo da informação utilizada para nomear os arquivos.
+ Otimizar as imagens renomeadas, visando reduzir a resolução, a qualidade e consequentemente o tamanho dos arquivos.
+ Tratar erros como nomes incorretos, falta de correlação entre arquivos e planilha, arquivos invalidos, etc.
+ Também tem o objetivo pessoal de ser uma forma de estudar e praticar programação.
