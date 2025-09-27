// Em cadastro.js

// 1. Espera o HTML ser completamente carregado para executar o script
document.addEventListener('DOMContentLoaded', function () {


    

    // 2. Pega os elementos do HTML pelos seus IDs
    const form = document.getElementById('form-cadastro');
    const mensagemDiv = document.getElementById('mensagem');

    // 3. Adiciona um "escutador" para o evento de 'submit' do formulário
    form.addEventListener('submit', function (event) {
        
        // 4. Previne o comportamento padrão do formulário, que é recarregar a página
        event.preventDefault();

        // 5. Coleta os dados dos campos do formulário
        const dadosDoUsuario = {
            nome: document.getElementById('nome').value,
            email: document.getElementById('email').value,
            senha: document.getElementById('senha').value
        };

        // Mostra os dados no console para depuração (opcional)
        console.log('Dados a serem enviados:', dadosDoUsuario);

        // 6. Envia os dados para a API usando o 'fetch'
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
                return response.json().then(err => { throw new Error(err.title || 'Ocorreu um erro no cadastro.') });
            }
            return response.json(); // Converte a resposta da API de JSON para objeto
        })
        .then(data => {
            // 8. Se tudo deu certo (caiu aqui), mostra uma mensagem de sucesso
            console.log('Sucesso:', data);
            mensagemDiv.textContent = 'Usuário cadastrado com sucesso! ID: ' + data.id;
            mensagemDiv.style.color = 'green';
            form.reset(); // Limpa o formulário
        })
        .catch(error => {
            // 9. Se algo deu errado (caiu aqui), mostra uma mensagem de erro
            console.error('Erro:', error);
            mensagemDiv.textContent = 'Erro ao cadastrar: ' + error.message;
            mensagemDiv.style.color = 'red';
        });
    });
});