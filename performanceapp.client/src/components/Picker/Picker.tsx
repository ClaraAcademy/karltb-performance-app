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
  return (
    <div className="picker-group">
      <label>
        <p>{label}</p>
        <select value={selected} onChange={onChange} style={{ width: "100%" }}>
          <option value="" disabled hidden>
            {placeholder ?? ""}
          </option>
          {values.map((p) => (
            <option key={p.key} value={p.key}>
              {p.value}
            </option>
          ))}
        </select>
      </label>
    </div>
  );
}
