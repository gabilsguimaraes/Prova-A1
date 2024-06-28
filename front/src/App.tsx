import React from 'react';
import { BrowserRouter, Link, Routes, Route } from "react-router-dom";
import CadastroTarefa from "./components/tarefa/tarefa-listar";
import ListaTarefa from "./components/tarefa/tarefa-cadastrar";
import './App.css';
function App() {
  return (
    <div>
      <BrowserRouter>
        <nav>
          <ul>
            <li>
              <Link to={"/"}>Home</Link>
            </li>
            <li>
              <Link to={"/pages/tarefa/listar"}>
                Listar Tarefas{" "}
              </Link>
            </li>
            <li>
              <Link to={"/pages/tarefa/cadastrar"}>
                Cadastrar Tarefas{" "}
              </Link>
            </li>
          </ul>
        </nav>
        <Routes>
          <Route path="/" element={<ListaTarefa />} />
          <Route
            path="/pages/tarefa/listar"
            element={<ListaTarefa />}
          />
          <Route
            path="/pages/tarefa/cadastrar"
            element={<CadastroTarefa />}
          />
          
        </Routes>
      </BrowserRouter>
    </div>
  );
}

export default App;
