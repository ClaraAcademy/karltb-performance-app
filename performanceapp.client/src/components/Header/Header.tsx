import LogoutButton from "../Auth/LogoutButton";
import "./Header.css";

interface HeaderProps {
  children?: React.ReactNode;
}

export default function Header({ children }: HeaderProps) {
  return (
    <header className="header">
      <div className="header-container">
        <div className="header-text">
          <h1>Performance App</h1>
        </div>
        {children}
        <LogoutButton />
      </div>
    </header>
  );
}
