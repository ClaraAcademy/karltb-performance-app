import { useAuth } from "../../contexts/AuthContext";
import "./LogoutButton.css";

export default function LogoutButton() {
  const { logout } = useAuth();

  return (
    <button onClick={logout} className="logout-button">
      Logout
    </button>
  );
}
