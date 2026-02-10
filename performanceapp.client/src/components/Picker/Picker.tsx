import type { KeyValue, OnChange } from "../../types";

interface PickerProps {
  selected: string;
  onChange: OnChange;
  values: KeyValue<string, string>[];
  placeholder?: string;
}

export default function Picker(props: PickerProps) {
  const { selected, onChange, values, placeholder } = props;
  return (
    <select value={selected} onChange={onChange}>
      <option value="" disabled hidden>
        {placeholder ?? ""}
      </option>
      {values.map((p) => (
        <option key={p.key} value={p.key}>
          {p.value}
        </option>
      ))}
    </select>
  );
}
