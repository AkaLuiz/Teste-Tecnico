import { Navigate } from "react-router-dom";

interface Props {
  children: React.ReactNode;
}

export default function ProtectedRoute({ children }: Props) {
  const token = localStorage.getItem("token");

  const expiration = localStorage.getItem("tokenExpiration");

  if (!token || !expiration) {
    return <Navigate to="/" replace />;
  }

  return <>{children}</>;
}
