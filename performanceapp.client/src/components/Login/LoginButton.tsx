const TYPE = "submit";
const CLASS_NAME = "login-button";

export default function LoginButton() {
  return (
    <button type={TYPE} className={CLASS_NAME}>
      Login
    </button>
  );
}
