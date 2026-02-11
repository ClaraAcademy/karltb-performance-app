interface PortfolioInfoProps {
  header: string;
  name: string;
}
export default function PortfolioInfo(props: PortfolioInfoProps) {
  const { header, name } = props;
  return (
    <div className="cell portfolioInfo" id="portfolioLabel">
      <div className="subGridWrapper">
        <div className="labelHeader">{header}</div>
        <div className="labelName">{name}</div>
      </div>
    </div>
  );
}
