import { Routes, Route } from "react-router-dom";
import Login from "../pages/LoginPage";
import RegistrosPage from "../pages/RegistrosPage";
import RegistroFormPage from "../pages/RegistroFormPage";
import ProtectedRoute from "../components/ProtectedRoute";

export default function AppRoutes() {
  return (
    <Routes>
      <Route path="/" element={<Login />} />
      <Route
        path="/registros"
        element={
          <ProtectedRoute>
            <RegistrosPage />
          </ProtectedRoute>
        }
      />
      <Route
        path="/registros/novo"
        element={
          <ProtectedRoute>
            <RegistroFormPage />
          </ProtectedRoute>
        }
      />
      <Route
        path="/registros/editar/:id"
        element={
          <ProtectedRoute>
            <RegistroFormPage />
          </ProtectedRoute>
        }
      />
    </Routes>
  );
}
