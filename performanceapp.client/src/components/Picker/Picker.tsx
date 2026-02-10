import type { KeyValue } from "../../types";

interface PickerProps {
  selected: string;
  onChange: (e: React.ChangeEvent<HTMLSelectElement>) => void;
  placeholder: string;
  values: KeyValue<string, string>[];
}

export default function Picker(props: PickerProps) {
  const { selected, onChange, placeholder, values } = props;
  return (
    <select value={selected} onChange={onChange}>
      {/* Placeholder that disappears after choice */}
      <option value="" disabled hidden>
        {placeholder}
      </option>
      {values.map((p) => (
        <option key={p.key} value={p.key}>
          {p.value}
        </option>
      ))}
    </select>
  );
}
