// Em cadastro.js

// 1. Espera o HTML ser completamente carregado para executar o script
document.addEventListener('DOMContentLoaded', function () {

    // 2. Pega os elementos do HTML pelos seus IDs
    // **** CORRIGIDO: O ID do formulário agora é 'form-cadastro' ****
    const form = document.getElementById('form-cadastro');
    
    // **** CORRIGIDO: O ID da div de mensagem agora é 'mensagem-status' ****
    const mensagemDiv = document.getElementById('mensagem-status');

    // 3. Adiciona um "escutador" para o evento de 'submit' do formulário
    form.addEventListener('submit', function (event) {
        
        // 4. Previne o comportamento padrão do formulário, que é recarregar a página
        event.preventDefault();

        // 5. Coleta os dados dos campos do formulário
        // **** CORRIGIDO: IDs com letra maiúscula e todos os campos adicionados ****
        const dadosDoUsuario = {
            // As chaves (Nome, Email...) devem ser IGUAIS às do seu Models/Usuario.cs
            Nome: document.getElementById('Nome').value,
            Email: document.getElementById('Email').value,
            Senha: document.getElementById('Senha').value,
            CPF: document.getElementById('CPF').value,
            Telefone: document.getElementById('Telefone').value,
            DatadeNascimento: document.getElementById('DataNascimento').value
        };

        // Mostra os dados no console para depuração (opcional)
        console.log('Dados a serem enviados:', dadosDoUsuario);

        // 6. Envia os dados para a API usando o 'fetch'
        // A URL /api/Usuarios está CORRETA, pois seu controller é [Route("api/[controller]")]
        fetch('/api/Usuarios', {
            method: 'POST', // O método HTTP
            headers: {
                'Content-Type': 'application/json' // Informa que o corpo da requisição é JSON
            },
            body: JSON.stringify(dadosDoUsuario) // Converte o objeto JavaScript em uma string JSON
        })
        .then(response => {
            // 7. Verifica se a resposta da API foi bem-sucedida
            if (!response.ok) {
                // Se não foi OK (ex: erro 400 ou 500), lança um erro para cair no .catch()
                return response.text().then(text => { throw new Error('Erro do servidor: ' + text) });
            }
            return response.json(); // Converte a resposta da API de JSON para objeto
        })
        .then(data => {
            // 8. Se tudo deu certo (caiu aqui):
            console.log('Sucesso Tela 1:', data);
            
            // 9. SALVA o ID do usuário no navegador
            // (sessionStorage é limpo quando o navegador fecha)
            sessionStorage.setItem('usuarioIdParaCadastro', data.id);

            // 10. REDIRECIONA para a tela 2
            window.location.href = '/TelaCadastro2';
        })
        .catch(error => {
            // 9. Se algo deu errado (caiu aqui), mostra uma mensagem de erro
            console.error('Erro:', error);
            mensagemDiv.textContent = 'Erro ao cadastrar: ' + error.message;
            mensagemDiv.style.color = 'red';
        });
    });
});