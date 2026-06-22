import { Link } from "react-router-dom";
import { useEffect, useState } from "react";
import axios from "axios";
import formatarCpfCnpj from "../utils/formatarCpfCnpj";
import Logout from "../components/logout";

const tipos = ["Contrato", "Procuracao", "Notificacao"];

const status = ["Pendente", "Registrado", "Devolvido"];

interface Registro {
  id: string;
  tipo: number;
  nomeApresentante: string;
  cpfCnpj: string;
  dataEntrada: string;
  status: number;
  observacoes: string;
  criadoPor: string;
  criadoEm: string;
  atualizadoEm: string;
  criadoPorNome: string;
}

interface Usuario {
  id: string;
  name: string;
  email: string;
  usuarioPapel: number;
}

export default function RegistrosPage() {
  const [page, setPage] = useState(1);
  const [statusPesquisa, setStatusPesquisa] = useState<number | null>(null);
  const [tipo, setTipo] = useState<number | null>(null);
  const token = localStorage.getItem("token");

  async function carregarUsuarios() {
    //Carregar dados do usuário logado
    const responseUsuario = await axios.get<Usuario>(
      "http://localhost:5167/auth/me",
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      },
    );

    setUsuario(responseUsuario.data);
  }

  async function carregarRegistros() {
    //Carrega todos os registros
    const responseTodosRegistros = await axios.get(
      "http://localhost:5026/registros/todos",
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
        params: {},
      },
    );

    setTodosRegistros(responseTodosRegistros.data);

    //Carrega os 10 primeiros registros
    const responseRegistros = await axios.get(
      "http://localhost:5026/registros",
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
        params: {
          status: statusPesquisa,
          tipo: tipo,
          page: page,
        },
      },
    );

    setRegistros(responseRegistros.data);
  }

  async function deletarRegistro(id: string) {
    const confirmar = window.confirm("Deseja realmente excluir este registro?");

    if (!confirmar) {
      return;
    }

    await axios.delete(`http://localhost:5026/registros/${id}`, {
      headers: {
        Authorization: `Bearer ${token}`,
      },
    });

    carregarRegistros();
  }

  async function alterarStatus(
    id: string,
    novoStatus: number,
    mensagem: string,
  ) {
    const confirmar = window.confirm(mensagem);

    if (!confirmar) {
      return;
    }

    await axios.patch(
      `http://localhost:5026/registros/${id}/status`,
      {
        status: novoStatus,
      },
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      },
    );

    carregarRegistros();
  }

  const [registros, setRegistros] = useState<Registro[]>([]);
  const [todosRegistros, setTodosRegistros] = useState<Registro[]>([]);
  const [usuario, setUsuario] = useState<Usuario | null>(null);

  useEffect(() => {
    carregarUsuarios();
  }, []);

  useEffect(() => {
    carregarRegistros();
  }, [page, statusPesquisa, tipo]);

  return (
    <>
      <Logout value={usuario?.name} />
      <div className="container">
        <h2>Registros</h2>

        <div className="d-flex align-items-center gap-2 mb-3">
          {usuario &&
            (usuario.usuarioPapel === 0 || usuario.usuarioPapel === 1) && (
              <Link to="/registros/novo" className="btn btn-primary">
                Criar Registro
              </Link>
            )}
          <button
            className="btn btn-primary"
            onClick={() => [setStatusPesquisa(null), setTipo(null)]}
          >
            Limpar filtros
          </button>
        </div>

        <div className="d-flex gap-2 mt-3">
          <label>Tipo</label>
          <button
            className={tipo === 0 ? "btn btn-primary" : "btn btn-secondary"}
            onClick={tipo === 0 ? () => setTipo(null) : () => setTipo(0)}
          >
            Contratos
          </button>

          <button
            className={tipo === 1 ? "btn btn-primary" : "btn btn-secondary"}
            onClick={tipo === 1 ? () => setTipo(null) : () => setTipo(1)}
          >
            Procurações
          </button>

          <button
            className={tipo === 2 ? "btn btn-primary" : "btn btn-secondary"}
            onClick={tipo === 2 ? () => setTipo(null) : () => setTipo(2)}
          >
            Notificações
          </button>
        </div>

        <div className="d-flex gap-2 mt-3">
          <label>Status</label>
          <button
            className={
              statusPesquisa === 0 ? "btn btn-primary" : "btn btn-secondary"
            }
            onClick={
              statusPesquisa === 0
                ? () => setStatusPesquisa(null)
                : () => setStatusPesquisa(0)
            }
          >
            Pendentes
          </button>

          <button
            className={
              statusPesquisa === 1 ? "btn btn-primary" : "btn btn-secondary"
            }
            onClick={
              statusPesquisa === 1
                ? () => setStatusPesquisa(null)
                : () => setStatusPesquisa(1)
            }
          >
            Registrados
          </button>

          <button
            className={
              statusPesquisa === 2 ? "btn btn-primary" : "btn btn-secondary"
            }
            onClick={
              statusPesquisa === 2
                ? () => setStatusPesquisa(null)
                : () => setStatusPesquisa(2)
            }
          >
            Devolvidos
          </button>
        </div>

        <div className="table-responsive-md">
          <table className="table table-striped table-responsive">
            <thead>
              <tr>
                <th>Tipo</th>
                <th>Apresentante</th>
                <th>CPF/CNPJ</th>
                <th>Data Entrada</th>
                <th>Status</th>
                <th>Observações</th>
                <th>Criado Por</th>
                <th className="text-center">Criado Em</th>
                <th className="text-center">Atualizado Em</th>
                {usuario && usuario.usuarioPapel < 2 && (
                  <th className="text-center">Ações</th>
                )}
              </tr>
            </thead>

            <tbody>
              {registros.map((registro) => (
                <tr key={registro.id}>
                  <td>{tipos[registro.tipo]}</td>
                  <td>{registro.nomeApresentante}</td>
                  <td>{formatarCpfCnpj(registro.cpfCnpj)}</td>
                  <td>{registro.dataEntrada.split("-").reverse().join("/")}</td>
                  <td>{status[registro.status]}</td>
                  <td style={{ maxWidth: "250px" }}>{registro.observacoes}</td>
                  <td>{registro.criadoPorNome}</td>
                  <td className="text-center">
                    {new Date(registro.criadoEm).toLocaleString("pt-BR")}
                  </td>
                  <td>
                    {new Date(registro.atualizadoEm).toLocaleString("pt-BR")}
                  </td>
                  <td>
                    <div className="d-flex gap-2">
                      {usuario && usuario.usuarioPapel === 0 && (
                        <button
                          className="btn btn-danger"
                          onClick={() => deletarRegistro(registro.id)}
                        >
                          <i className="bi bi-trash3-fill"></i>
                        </button>
                      )}
                      {usuario &&
                        (usuario.usuarioPapel === 0 ||
                          usuario.usuarioPapel === 1) && (
                          <Link
                            className="btn btn-warning"
                            to={`/registros/editar/${registro.id}`}
                          >
                            <i className="bi bi-pencil-fill"></i>
                          </Link>
                        )}
                      {registro.status === 0 &&
                        usuario &&
                        usuario.usuarioPapel != 2 && (
                          <>
                            <button
                              className="btn btn-secondary"
                              onClick={() =>
                                alterarStatus(
                                  registro.id,
                                  2,
                                  "Deseja realmente devolver este registro?",
                                )
                              }
                            >
                              Devolver
                            </button>

                            <button
                              className="btn btn-success"
                              onClick={() =>
                                alterarStatus(
                                  registro.id,
                                  1,
                                  "Deseja realmente devolver este registro?",
                                )
                              }
                            >
                              Registrar
                            </button>
                          </>
                        )}
                      {registro.status === 2 &&
                        usuario &&
                        usuario.usuarioPapel != 2 && (
                          <button
                            className="btn btn-info"
                            onClick={() =>
                              alterarStatus(
                                registro.id,
                                0,
                                "Deseja realmente devolver este registro?",
                              )
                            }
                          >
                            Reapresentar
                          </button>
                        )}
                    </div>
                  </td>
                </tr>
              ))}
            </tbody>
          </table>
        </div>
        <div className="d-flex gap-2 mt-3">
          <button
            className={page === 1 ? "btn btn-secondary" : "btn btn-primary"}
            disabled={page === 1}
            onClick={() => setPage(page - 1)}
          >
            Anterior
          </button>

          <span className="align-self-center">Página {page}</span>

          <button
            className={
              todosRegistros.length < page*10
                ? "btn btn-secondary"
                : "btn btn-primary"
            }
            disabled={todosRegistros.length < page*10}
            onClick={() => setPage(page + 1)}
          >
            Próxima
          </button>
        </div>
      </div>
    </>
  );
}
