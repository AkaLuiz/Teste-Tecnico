import { useState, useEffect } from "react";
import axios from "axios";
import { useParams, useNavigate, Link } from "react-router-dom";
import CpfCnpjInput from "../components/CpfCnpjInput";

export default function RegistroFormPage() {
  const token = localStorage.getItem("token");
  const { id } = useParams();
  const [tipo, setTipo] = useState(0);
  const [nomeApresentante, setNomeApresentante] = useState("");
  const [cpfCnpj, setCpfCnpj] = useState("");
  const [dataEntrada, setDataEntrada] = useState("");
  const [observacoes, setObservacoes] = useState("");
  const [erro, setErro] = useState("");
  const navigate = useNavigate();

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();

    console.log({
      tipo,
      nomeApresentante,
      cpfCnpj,
      dataEntrada,
      observacoes,
    });

    setErro("");

    try {
      if (id) {
        const confirmar = window.confirm(
          "Deseja realmente editar este registro?",
        );

        if (!confirmar) {
          return;
        }
        await axios.put(
          `http://localhost:5026/registros/${id}`,
          {
            tipo,
            nomeApresentante,
            cpfCnpj,
            dataEntrada,
            observacoes,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          },
        );
      } else {
        const confirmar = window.confirm(
          "Deseja realmente criar este registro?",
        );

        if (!confirmar) {
          return;
        }
        await axios.post(
          "http://localhost:5026/registros",
          {
            tipo,
            nomeApresentante,
            cpfCnpj,
            dataEntrada,
            observacoes,
          },
          {
            headers: {
              Authorization: `Bearer ${token}`,
            },
          },
        );
      }
      navigate("/registros");
    } catch (error: unknown) {
      if (axios.isAxiosError(error)) {
        const data = error.response?.data;

        if (typeof data === "string") {
          setErro(data);
        } else if (data?.errors) {
          const primeiraChave = Object.keys(data.errors)[0];
          setErro(data.errors[primeiraChave][0]);
        } else {
          setErro(data?.title ?? "Ocorreu um erro.");
        }
      }
    }
  }

  async function carregarRegistro(id: string) {
    //Carregar todos os registros
    const responseRegistros = await axios.get(
      `http://localhost:5026/registros/${id}`,
      {
        headers: {
          Authorization: `Bearer ${token}`,
        },
      },
    );

    const registro = responseRegistros.data;

    setTipo(registro.tipo);
    setNomeApresentante(registro.nomeApresentante);
    setCpfCnpj(registro.cpfCnpj);
    setDataEntrada(registro.dataEntrada);
    setObservacoes(registro.observacoes);
  }

  useEffect(() => {
    if (!id) return;

    async function carregar() {
      await carregarRegistro(id!);
    }

    carregar();
  }, [id]);

  return (
    <div className="container mt-4">
      <h2>{id ? "Editar Registro" : "Novo Registro"}</h2>

      {erro && <div className="alert alert-danger">{erro}</div>}

      <form onSubmit={handleSubmit}>
        <div className="mb-3">
          <label>Tipo</label>

          <select
            className="form-select"
            value={tipo}
            onChange={(e) => setTipo(Number(e.target.value))}
          >
            <option value={0}>Contrato</option>

            <option value={1}>Procuração</option>

            <option value={2}>Notificação</option>
          </select>
        </div>

        <div className="mb-3">
          <label>Nome Apresentante</label>

          <input
            className="form-control"
            value={nomeApresentante}
            onChange={(e) => {
              setErro("");
              setNomeApresentante(e.target.value);
            }}
          />
        </div>

        <div className="mb-3">
          <label>CPF/CNPJ</label>

          <CpfCnpjInput value={cpfCnpj} onChange={setCpfCnpj} />
        </div>

        <div className="mb-3">
          <label>Data Entrada</label>

          <input
            type="date"
            className="form-control"
            value={dataEntrada}
            onChange={(e) => setDataEntrada(e.target.value)}
          />
        </div>

        <div className="mb-3">
          <label>Observações</label>

          <textarea
            className="form-control"
            value={observacoes}
            onChange={(e) => setObservacoes(e.target.value)}
          />
        </div>
        <div className="d-flex gap-2">
          <button type="submit" className="btn btn-success">
            Salvar
          </button>
          <Link to="/registros" className="btn btn-secondary">
            Voltar
          </Link>
        </div>
      </form>
    </div>
  );
}
