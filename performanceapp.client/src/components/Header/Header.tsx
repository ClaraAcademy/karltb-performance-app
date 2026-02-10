import "./Header.css";

interface HeaderProps {
  onLogout?: () => void;
  children?: React.ReactNode;
}

export default function Header({ onLogout, children }: HeaderProps) {
  return (
    <header className="header">
      <div className="header-container">
        <div className="header-text">
          <h1>Performance App</h1>
        </div>
        {children}
        <div className="header-logout">
          <button onClick={onLogout}>Logout</button>
        </div>
      </div>
    </header>
  );
}
