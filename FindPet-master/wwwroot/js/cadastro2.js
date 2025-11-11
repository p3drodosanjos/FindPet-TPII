// Em wwwroot/js/cadastro2.js

document.addEventListener('DOMContentLoaded', function () {

    // 1. Pega o formulário da Tela 2 (iremos dar esse ID no passo 5)
    const form = document.getElementById('form-cadastro-endereco');
    
    // 2. Pega a div de mensagem (iremos dar esse ID no passo 5)
    const mensagemDiv = document.getElementById('mensagem-status-2');

    form.addEventListener('submit', function (event) {
        event.preventDefault(); // Previne o recarregamento da página

        // 3. Pega o ID do usuário que salvamos na Tela 1
        const usuarioId = sessionStorage.getItem('usuarioIdParaCadastro');

        if (!usuarioId) {
            mensagemDiv.textContent = 'Erro: ID do usuário não encontrado. Volte para a tela 1.';
            mensagemDiv.style.color = 'red';
            return;
        }

        // 4. Coleta os dados de ENDEREÇO
        // Os IDs (CEP, Rua...) devem bater com o HTML da Tela 2
        // As chaves (CEP, Rua...) devem bater com o Models/Usuario.cs
        const dadosEndereco = {
            CEP: document.getElementById('CEP').value,
            Rua: document.getElementById('Rua').value,
            Bairro: document.getElementById('Bairro').value,
            Cidade: document.getElementById('Cidade').value,
            Numero: document.getElementById('Numero').value,
            Complemento: document.getElementById('Complemento').value,
            Estado: document.getElementById('Estado').value,
            Pais: document.getElementById('Pais').value
        };

        console.log('Enviando endereço para o ID:', usuarioId);
        console.log('Dados de endereço:', dadosEndereco);

        // 5. Envia os dados para o endpoint PUT /api/Usuarios/{id}
        fetch(`/api/Usuarios/${usuarioId}`, {
            method: 'PUT', // Método para ATUALIZAR
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(dadosEndereco)
        })
        .then(response => {
            if (!response.ok) {
                // Se der erro
                return response.text().then(text => { throw new Error('Erro do servidor: ' + text) });
            }
            // Se der certo (resposta 204 No Content)
            return response;
        })
        .then(() => {
            // 6. Sucesso! Limpa o ID e redireciona para o Login
            console.log('Endereço salvo! Redirecionando para Login...');
            sessionStorage.removeItem('usuarioIdParaCadastro'); // Limpa o ID
            window.location.href = '/TelaLogin'; // Manda para o Login
        })
        .catch(error => {
            // 7. Se algo deu errado
            console.error('Erro:', error);
            mensagemDiv.textContent = 'Erro ao salvar endereço: ' + error.message;
            mensagemDiv.style.color = 'red';
        });
    });
});