import { useAuth } from "../contexts/AuthContext";
import MainApp from "../components/MainApp";
import LoginForm from "../components/Login/LoginForm";

export default function App() {
  const { login, isAuthenticated } = useAuth();

  async function handleLogin(username: string, password: string) {
    return await login(username, password);
  }

  if (!isAuthenticated) {
    return <LoginForm onLogin={handleLogin} />;
  }

  return <MainApp />;
}
