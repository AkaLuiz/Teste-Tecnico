import { useNavigate } from "react-router-dom";

interface Props {
  value: string;
}

export default function Navbar({ value }: Props) {
  const navigate = useNavigate();

  function logout() {
    localStorage.removeItem("token");
    localStorage.removeItem("tokenExpiration");

    navigate("/");
  }

  return (
    <nav className="navbar navbar-dark bg-dark px-3">
      <span className="navbar-brand">Cartório</span>
      <div>
        {" "}
        <span className="navbar-brand">{value}</span>
        <button className="btn btn-outline-danger" onClick={logout}>
          Sair
        </button>
      </div>
    </nav>
  );
}
