import { useEffect } from "react";
import { Navigate } from "react-router-dom";

interface Props {
  children: React.ReactNode;
}

export default function ProtectedRoute({ children }: Props) {
  const token = localStorage.getItem("token");
  const expiration = Number(localStorage.getItem("tokenExpiration"));
  const agora = new Date().getTime()
  const expirado = !token || !expiration || agora > expiration;

  useEffect(() => {
    if (expirado) {
      localStorage.removeItem("token");
      localStorage.removeItem("tokenExpiration");
    }
  }, [expirado]);

  if (expirado) {
    return <Navigate to="/" replace />;
  }

  return <>{children}</>;
}
