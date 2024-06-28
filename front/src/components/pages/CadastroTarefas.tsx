import React, { useState } from 'react';
import { Produto } from '../interfaces/Tarefa';

function CadastroTarefa() {
    const [titulo, setTitulo] = useState<string>('');
    const [descricao, setDescricao] = useState<string>('');
    const [status, setStatus] = useState<string>('');

    function handleSubmit (e: any) {
        e.preventDefault();

        const novaTarefa = {
            titulo,
            descricao,
            status
        };

        fetch('', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(novaTarefa)
        })
        .then(response => {
            if (!response.ok) {
                throw new Error('Erro na requisição: ' + response.statusText);
            }
            return response.json();
        })
        .then(data => {
            setTitulo('');
            setDescricao('');
            setStatus('');
        })
        .catch(error => {
            console.error('Erro:', error);
        });
    };

    return (
        <div>
            <h2>Cadastrar Nova Tarefa</h2>
            <form onSubmit={handleSubmit}>
                <label>
                    Título:
                    <input type="text" value={titulo} onChange={e => setTitulo(e.target.value)} required />
                </label>
                <label>
                    Descrição:
                    <input type="text" value={descricao} onChange={e => setDescricao(e.target.value)} required />
                </label>
                <label>
                    Status:
                    <input type="text" value={status} onChange={e => setStatus(e.target.value)} required />
                </label>
                
                <button type="submit">Cadastrar</button>
            </form>
        </div>
    );
};

export default CadastroTarefa;
