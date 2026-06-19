import { useState } from "react";
import { useNavigate } from "react-router-dom";
import axios from "axios";

export default function Login() {
    const navigate = useNavigate();

    const [email, setEmail] = useState("");
    const [senha, setSenha] = useState("");
    const [erro, setErro] = useState("");
    const [loading, setLoading] = useState(false);

    async function handleLogin(e: React.FormEvent) {
        e.preventDefault();

        setErro("");
        setLoading(true);

        try {
            const response = await axios.post(
                "http://localhost:5167/auth/login",
                {
                    email,
                    senha
                }
            );

            localStorage.setItem(
                "token",
                response.data.token
            );

            navigate("/registros");
        }
        catch {
            setErro("Email ou senha inválidos.");
        }
        finally {
            setLoading(false);
        }
    }

    return (
        <div className="container mt-5">
            <div className="row justify-content-center">
                <div className="col-md-4">

                    <div className="card">
                        <div className="card-body">

                            <h2 className="text-center mb-4">
                                Login
                            </h2>

                            <form onSubmit={handleLogin}>

                                <div className="mb-3">
                                    <label className="form-label">
                                        Email
                                    </label>

                                    <input
                                        type="email"
                                        className="form-control"
                                        value={email}
                                        onChange={(e) =>
                                            setEmail(e.target.value)
                                        }
                                        required
                                    />
                                </div>

                                <div className="mb-3">
                                    <label className="form-label">
                                        Senha
                                    </label>

                                    <input
                                        type="password"
                                        className="form-control"
                                        value={senha}
                                        onChange={(e) =>
                                            setSenha(e.target.value)
                                        }
                                        required
                                    />
                                </div>

                                {erro && (
                                    <div className="alert alert-danger">
                                        {erro}
                                    </div>
                                )}

                                <button
                                    type="submit"
                                    className="btn btn-primary w-100"
                                    disabled={loading}
                                >
                                    {loading
                                        ? "Entrando..."
                                        : "Entrar"}
                                </button>

                            </form>

                        </div>
                    </div>

                </div>
            </div>
        </div>
    );
}