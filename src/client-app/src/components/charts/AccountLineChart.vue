<template>
  <div>
    <v-chart ref="chart" :option="option" class="lineChart" autoresize />
    <button @click="addItem">新增項目</button>
  </div>
</template>

<script lang="ts">
import { EChartsOption, init } from "echarts";
import { Component, Prop, Ref, Vue } from "vue-property-decorator";
import { IAccountLineChartDto } from "@/apis/apiModels";
import { TopLevelFormatterParams } from "echarts/types/dist/shared";

@Component({
  components: {},
})
export default class AccountLineChart extends Vue {
  @Ref("chart")
  private chart: any;

  @Prop({ type: Array, default: () => [] })
  private data!: IAccountLineChartDto[];

  // 使用強型別幫助識別
  get option(): EChartsOption {
    return {
      tooltip: {
        show: true,
        formatter: function (params: any) {
          return `年齡範圍: ${params.value[0]}, 平均金額: ${Math.floor(
            params.value[1]
          )}`;
        },
      },
      grid: {
        top: "20%",
        left: "3%",
        right: "5%",
        bottom: "3%",
        containLabel: true,
      },
      toolbox: {
        feature: {
          saveAsImage: {},
        },
      },
      xAxis: {
        type: "category",
        name: "年齡範圍",
      },
      yAxis: {
        type: "value",
        name: "平均金額",
      },
      series: [
        {
          type: "line",
          // data: [
          //   ["2021-05-01", 2],
          //   ["2021-05-11", 5],
          //   ["2021-05-18", 2],
          //   ["2021-05-22", 1],
          // ],
          data: this.data.map((d) => [d.ageRange || 0, d.averageBalance || 0]),
        },
      ],
    };
  }

  addItem() {
    console.log(this.data);
    console.log(this.option.series);
  }
}
</script>

<style>
.lineChart {
  height: 450px;
}
</style>