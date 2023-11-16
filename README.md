# Servidor UDP


#Configuração do Ambiente
**Baixe o Visual Studio:**

Faça o download do Visual Studio aqui:https://visualstudio.microsoft.com/pt-br/downloads/.
Siga as instruções de instalação.
**Adicione os Arquivos ao Visual Studio:**

Coloque os arquivos **Servidor.cs** e **Cliente.cs** dentro do **Visual Studio.
Instale as Extensões Necessárias:**

Vá para **"Ferramentas"** e selecione** "Gerenciador de Pacotes do NuGet".**
No **Gerenciador de Pacotes do NuGet**, escolha **"Gerenciador de Pacotes do NuGet para a Solução".**
# Servidor
**Instale Pacotes no Servidor:**

Dentro do Gerenciador de Pacotes do NuGet para a Solução, clique em **"Instalar"** e adicione os seguintes pacotes para o código do Servidor:
-MySqlConnection-

-MySQL-

-SQLite-
**Execute o Servidor:**

Abra o código do Servidor no Visual Studio.
Rode o **Servidor antes do Cliente** para garantir uma conexão adequada.
# Cliente
**Instale Pacotes no Cliente:**

No Gerenciador de Pacotes do NuGet para a Solução, clique em "Instalar" e adicione os seguintes pacotes para o código do Cliente:
-MySqlConnection-
-MySQL-
**Execute o Cliente:**

Abra o código do Cliente no Visual Studio.
Rode o Cliente após o Servidor estar em execução.
Agora, você está pronto para começar! Lembre-se de seguir esta ordem: Servidor primeiro e depois o Cliente. Se você encontrar algum problema, verifique se todos os pacotes foram instalados corretamente e se o Servidor está em execução antes do Cliente.


# Configuração do Banco de Dados MySQL
**O guia para configurar o banco de dados MySQL para a aplicação.**

Instalação do Banco de Dados MySQL
Baixe o MySQL 

Faça o download do MySQL: **https://dev.mysql.com/downloads/installer/**.
Siga as instruções de instalação.
**Assistência à Instalação:**

Em caso de dúvidas durante a instalação, consulte este vídeo tutorial:**https://www.youtube.com/watch?v=whSimU8GfhY&t=1390s.** Minuto 9:14 em Diante
**Configuração do Usuário:**

Após a instalação, configure o **usuário como root** e a **senha como 123456** Todos os detalhes estão salvos no arquivo do **banco de dados**.
Configuração Automática da Tabela
**Abra o Banco de Dados:**

Abra o MySQL e certifique-se de que está em execução.
**Execute o Código:**

Execute o código do programa. Se a tabela não for criada automaticamente, dê um **refresh no código**.
**Localização do Arquivo JSON**
Procure um arquivo chamado **"dev_serve"**. Normalmente, ele está localizado na pasta **bin**, **"Debug"** dentro dos arquivos do programa. Se não encontrar, execute os códigos do servidor e do cliente, pois ele será gerado automaticamente dentro da pasta **Debug**.
Agora, seu banco de dados MySQL está configurado e pronto para ser utilizado pelo programa. Se encontrar algum problema, siga os passos acima e garanta que o MySQL esteja em execução antes de executar o código.
