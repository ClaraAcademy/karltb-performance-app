import type { KeyValue, OnChange } from "../../types";
import "./Picker.css";

interface PickerProps {
  label: string;
  selected: string;
  onChange: OnChange;
  values: KeyValue<string, string>[];
  placeholder?: string;
}

export default function Picker({
  label,
  selected,
  onChange,
  values,
  placeholder,
}: PickerProps) {
  const id = `${label.toLowerCase()}-picker`;
  return (
    <div className="picker-group">
      <label htmlFor={id} className="picker-cell">{label}</label>
      <select
        value={selected}
        onChange={onChange}
        style={{ width: "100%" }}
        id={id}
        className="picker-cell"
      >
        <option value="" disabled hidden>
          {placeholder ?? ""}
        </option>
        {values.map((p) => (
          <option key={p.key} value={p.key}>
            {p.value}
          </option>
        ))}
      </select>
    </div>
  );
}
