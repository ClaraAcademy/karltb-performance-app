import { HEIGHT, WIDTH } from "../../enums/SvgDimensions";

interface SvgProps {
  content: string;
}

const EMPTY = <img className="svg" style={{ width: WIDTH, height: HEIGHT }} />;

export default function Svg({ content }: SvgProps) {
  if (!content) {
    return EMPTY;
  }
  return <img className="svg" src={content} alt="Chart" />;
}
