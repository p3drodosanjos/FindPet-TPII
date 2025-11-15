// Espera o documento carregar
document.addEventListener("DOMContentLoaded", () => {
    
    // Pega os campos do formulário (AGORA CORRIGIDOS com letra maiúscula)
    const cepInput = document.getElementById("CEP");
    const ruaInput = document.getElementById("Rua");
    const bairroInput = document.getElementById("Bairro");
    const cidadeInput = document.getElementById("Cidade");
    const estadoInput = document.getElementById("Estado");

    // Adiciona um "ouvinte" que dispara quando o usuário TIRA O FOCO do campo CEP
    cepInput.addEventListener("blur", async () => {
        const cep = cepInput.value.replace("-", ""); // Limpa o CEP (tira o traço)

        if (cep.length === 8) { // Só busca se tiver 8 dígitos
            try {
                // 1. CHAMA O ENDPOINT QUE VOCÊ CRIOU!
                const response = await fetch(`/api/buscar-cep/${cep}`);

                if (response.ok) {
                    // 2. Converte a resposta (o JSON) para um objeto
                    const endereco = await response.json();
                    
                    // 3. PREENCHE OS CAMPOS!
                    ruaInput.value = endereco.rua;
                    bairroInput.value = endereco.bairro;
                    cidadeInput.value = endereco.cidade;
                    estadoInput.value = endereco.estado;
                } else {
                    alert("CEP não encontrado. Verifique e tente novamente.");
                    ruaInput.value = ""; // Limpa os campos
                    bairroInput.value = "";
                    cidadeInput.value = "";
                    estadoInput.value = "";
                }
            } catch (error) {
                console.error("Erro ao buscar CEP:", error);
                alert("Falha ao buscar o CEP. Tente novamente.");
            }
        }
    });
});