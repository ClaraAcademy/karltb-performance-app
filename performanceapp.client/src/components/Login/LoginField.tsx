import { LoginFieldType } from "../../enums/LoginFieldType";

const autoCompleteMap = {
  [LoginFieldType.Username]: "username",
  [LoginFieldType.Password]: "current-password",
};

const typeMap = {
  [LoginFieldType.Username]: "text",
  [LoginFieldType.Password]: "password",
};

function getAutoComplete(label: LoginFieldType) {
  return autoCompleteMap[label];
}

function getType(label: LoginFieldType) {
  return typeMap[label];
}

interface LoginFieldProps {
  id: string;
  value: string;
  label: LoginFieldType;
  onChange: (e: React.ChangeEvent<HTMLInputElement>) => void;
}

export default function LoginField({
  id,
  value,
  label,
  onChange,
}: LoginFieldProps) {
  return (
    <>
      <label htmlFor={id}>{label}</label>
      <input
        id={id}
        type={getType(label)}
        value={value}
        onChange={onChange}
        className="login-input"
        autoComplete={getAutoComplete(label)}
      />
    </>
  );
}
