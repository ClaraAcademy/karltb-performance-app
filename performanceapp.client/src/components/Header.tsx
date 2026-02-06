import "./Header.css";

interface HeaderProps {
  onLogout?: () => void;
}

const Header = ({ onLogout }: HeaderProps) => {
  return (
    <header className="header">
      <div className="header-container">
        <div className="header-text">
          <h1>Performance App</h1>
        </div>
        <div className="header-logout">
          <button onClick={onLogout}>Logout</button>
        </div>
      </div>
    </header>
  );
};

export default Header;
