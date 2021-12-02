import { AnswerVisualizationModel } from "@Models/Visualization";
import { Box, Typography } from "@mui/material";
import { SxProps } from "@mui/system";
import { Bar, BarChart, CartesianGrid, Cell, ResponsiveContainer, Tooltip, XAxis, YAxis } from "recharts";

import { card, text } from "./style";

interface Props {
  answers: Array<AnswerVisualizationModel>;
  sx?: SxProps;
}

function ResultVisualizationCard({ answers, sx }: Props) {
  const style = { ...card, ...sx };

  return (
    <Box sx={style}>
      {answers.map((a) => (
        <Box>
          <Typography sx={text}>{a.questionText}</Typography>
          <ResponsiveContainer width="50%" aspect={1.3}>
            <BarChart data={a.options}>
              <CartesianGrid />
              <XAxis dataKey="optionText" stroke="white" />
              <YAxis stroke="white" />
              <Tooltip
                contentStyle={{
                  borderRadius: 10,
                  backgroundColor: "#33e6e4",
                  border: "2px solid white"
                }}
                wrapperStyle={{ color: "white", fontSize: 18, fontWeight: 500 }}
                itemStyle={{ color: "white", fontSize: 18, fontWeight: 500 }}
                payload={[{value: "sss"}]}
              />
              <Bar dataKey="answersAmount" radius={[10, 10, 0, 0]}>
                {a.options.map((o, i) => (
                  <Cell key={`cell-${i}`} fill={o.isCorrect ? "#33e668" : "#c72a2a"} />
                ))}
              </Bar>
            </BarChart>
          </ResponsiveContainer>
        </Box>
      ))}
    </Box>
  );
}

export default ResultVisualizationCard;
