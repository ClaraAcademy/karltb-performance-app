import { useState, useEffect } from "react";
import { type KeyValue, type Portfolio } from "../../types";
import { fetchPortfolios } from "../../api/FetchPortfolio";
import { createKeyValuesFromPortfolios } from "../../Factories/KeyValueFactory";
import Picker from "../Picker/Picker";

interface PortfolioPickerProps {
  portfolio: Portfolio | null;
  setPortfolio: (portfolio: Portfolio | null) => void;
}

export default function PortfolioPicker(props: PortfolioPickerProps) {
  const { portfolio, setPortfolio } = props;

  const [portfolios, setPortfolios] = useState<Portfolio[]>([]);
  const [values, setValues] = useState<KeyValue<string, string>[]>([]);

  const selected = portfolio?.portfolioId.toString() ?? "";
  const placeholder = "Select a portfolio";

  async function fetchAndSetPortfolios() {
    const portfolios = await fetchPortfolios();
    setPortfolios(portfolios);
  }

  useEffect(() => {
    fetchAndSetPortfolios();
  }, [portfolio]);

  useEffect(() => {
    setValues(createKeyValuesFromPortfolios(portfolios));
  }, [portfolios]);

  const onChange = (e: React.ChangeEvent<HTMLSelectElement>) => {
    const id = Number(e.target.value);
    const selected = portfolios.find((p) => p.portfolioId === id) ?? null;
    setPortfolio(selected);
  };

  return (
    <Picker
      selected={selected}
      onChange={onChange}
      placeholder={placeholder}
      values={values}
    />
  );
}
