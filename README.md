#Servidor UDP
Configuração do Ambiente
Baixe o Visual Studio:

Faça o download do Visual Studio aqui.
Siga as instruções de instalação.
Adicione os Arquivos ao Visual Studio:

Coloque os arquivos Servidor.cs e Cliente.cs dentro do Visual Studio.
Instale as Extensões Necessárias:

Vá para "Ferramentas" e selecione "Gerenciador de Pacotes do NuGet".
No Gerenciador de Pacotes do NuGet, escolha "Gerenciador de Pacotes do NuGet para a Solução".
Servidor
Instale Pacotes no Servidor:

Dentro do Gerenciador de Pacotes do NuGet para a Solução, clique em "Instalar" e adicione os seguintes pacotes para o código do Servidor:
MySqlConnection
MySQL
SQLite
Execute o Servidor:

Abra o código do Servidor no Visual Studio.
Rode o Servidor antes do Cliente para garantir uma conexão adequada.
Cliente
Instale Pacotes no Cliente:

No Gerenciador de Pacotes do NuGet para a Solução, clique em "Instalar" e adicione os seguintes pacotes para o código do Cliente:
MySqlConnection
MySQL
Execute o Cliente:

Abra o código do Cliente no Visual Studio.
Rode o Cliente após o Servidor estar em execução.
Agora, você está pronto para começar! Lembre-se de seguir esta ordem: Servidor primeiro e depois o Cliente. Se você encontrar algum problema, verifique se todos os pacotes foram instalados corretamente e se o Servidor está em execução antes do Cliente.
 
