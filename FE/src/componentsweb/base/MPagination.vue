<template>
  <div class="m-paging">
    <div class="m-paging-left">
      <p>
        {{ resource.MPagination.Sum
        }}<strong class="total">{{ totalRecord }}</strong>
        {{ resource.MPagination.ToTalRecord }}
      </p>
    </div>
    <div class="m-paging-right">
      <div class="title">{{ resource.MPagination.Record }}</div>
      <div class="pagination-records">
        <MISADropDownList
          :data="this.data"
          propKey="key"
          propValue="value"
          @getPageSize="getPageSize"
          name="drop"
        ></MISADropDownList>
      </div>
      <div class="page-number">
        <div class="amount-record">
          <strong>{{
            totalRecord > 0 ? pageSize * (pageNumber - 1) + 1 : 0
          }}</strong>
          -
          <strong>{{
            pageSize * pageNumber > totalRecord
              ? totalRecord
              : pageSize * pageNumber
          }}</strong>
          {{ resource.MPagination.ToTalRecord }}
        </div>
        <div
          :class="{ no_active: pageNumber === 1 }"
          class="page-after page_pointer icon icon-prev"
          @click="changPage('prev')"
        ></div>
        <!-- <div
          class="page-center"
          @click="changPage(page)"
          v-for="(page, index) in pages"
          :key="index"
          :class="{
            is_active: page === pageNumber,
            page_pointer: typeof page === 'number',
          }"
        >
          {{ page }}
        </div> -->
        <div
          :class="{
            no_active:
              pageNumber === Math.ceil(totalRecord / pageSize) ||
              totalRecord === 0,
          }"
          class="page-before page_pointer icon icon-before"
          @click="changPage('after')"
        ></div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "MPagination",
  props: {
    pageNumber: {
      type: Number,
      default: 1,
    },
    pageSize: {
      type: Number,
      default: 20,
    },
    totalRecord: {
      type: Number,
      default: 0,
    },
  },
  data() {
    return {
      data: [
        {
          key: "10",
          value: "10",
        },
        {
          key: "20",
          value: "20",
        },
        {
          key: "30",
          value: "30",
        },
        {
          key: "50",
          value: "50",
        },
        {
          key: "100",
          value: "100",
        },
      ],
    };
  },
  methods: {
    /**
     * Hàm chuyển trang
     * @param {Trang hiện tại} page
     * @author: Nguyễn Văn Trúc (24/1/2024)
     */
    changPage(page) {
      if (page === this.helper.MPagination.Prev && this.pageNumber <= 1) {
        return;
      }
      if (
        page === this.helper.MPagination.After &&
        this.pageNumber >= this.totalPage
      ) {
        return;
      }
      if (page === this.helper.MPagination.Prev) {
        page = this.pageNumber - 1;
      }
      if (page === this.helper.MPagination.After) {
        page = this.pageNumber + 1;
      }
      if (typeof page === this.helper.MPagination.Number) {
        this.$emit("update:pageNumber", page);
      } else {
        return;
      }
      this.$emit("update:pageNumber", page);
    },
    /**
     * Cập nhật số lượng trang
     * @param {só lượng bản ghi trên trang} value
     * @author: Nguyễn Văn Trúc (24/1/2024)
     */
    getPageSize(value) {
      this.$emit("update:pageSize", value);
      this.$emit("update:pageNumber", 1);
    },
  },
  computed: {
    /**
     * Tổng số trang
     * @author: Nguyễn Văn Trúc (24/1/2024)
     */
    totalPage() {
      return Math.ceil(this.totalRecord / this.pageSize);
    },

    /**
     * Hiển thị số lượng trang
     * @author: Nguyễn Văn Trúc (24/1/2024)
     */
    pages() {
      let pages = [];
      for (let i = 1; i <= this.totalPage; i++) {
        if (
          i === 1 ||
          i === this.totalPage ||
          (i <= this.pageNumber + 2 && i >= this.pageNumber - 2)
        ) {
          pages.push(i);
        } else if (i === this.pageNumber + 3 || i === this.pageNumber - 3) {
          pages.push("...");
        }
      }
      return pages;
    },
  },
};
</script>

<style scoped>
.is_active {
  border: 1px solid grey;
  padding: 0 4px;
}
.page_pointer {
  cursor: pointer;
  filter: invert(100%) sepia(0%) saturate(7474%) hue-rotate(57deg)
    brightness(10%) contrast(106%);
}
.no_active {
  filter: none;
  cursor: default;
}
</style>
