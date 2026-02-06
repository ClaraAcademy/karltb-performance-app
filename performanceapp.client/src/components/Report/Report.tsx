const ANCHOR_URL =
  "https://app.powerbi.com/view?r=eyJrIjoiYzRmZDBiZWQtMzZjOC00YjJiLWI4ZmEtNzY1MTY3YmQwZTc4IiwidCI6IjUxZmZiOGE1LWZmZGUtNDQ5ZC05OWYzLTg5ZDc1OWFkOTI4NSIsImMiOjl9";
const ANCHOR_TARGET = "_blank";
const ANCHOR_REL = "noopener noreferrer";

const Report = () => {
  return (
    <div>
      <h2>Power BI</h2>
      <p>
        The PowerBI report is available{" "}
        <a href={ANCHOR_URL} target={ANCHOR_TARGET} rel={ANCHOR_REL}>
          here
        </a>
        .
      </p>
    </div>
  );
};

export default Report;
