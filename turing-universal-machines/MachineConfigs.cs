using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace turing_universal_machines
{
    internal class MachineConfigs
    {

        internal static dynamic C1011 =

                new Dictionary<string, Dictionary<string, Dictionary<string, object>>> {

                    {"b",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "-",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "Pe", "R", "Pe", "R", "P0", "R", "R", "P0", "L", "L"} },
                                    { "finalMConfig", "o" }
                                }
                            },
                        }
                    },

                    {"o",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "1",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R", "Px", "L", "L", "L" } },
                                    { "finalMConfig", "o" }
                                }
                            },

                            { "0",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { } },
                                    { "finalMConfig", "q" }
                                }
                            }
                        }
                    },

                    {"q",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "1",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R", "R" } },
                                    { "finalMConfig", "q" }
                                }
                            },

                            { "0",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R", "R" } },
                                    { "finalMConfig", "q" }
                                }
                            },

                            { "-",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "P1", "L" } },
                                    { "finalMConfig", "p" }
                                }
                            }


                        }
                    },

                    {"p",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "x",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "E", "R" } },
                                    { "finalMConfig", "q" }
                                }
                            },

                            { "e",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R" } },
                                    { "finalMConfig", "f" }
                                }
                            },

                            { "-",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "L", "L" } },
                                    { "finalMConfig", "p" }
                                }
                            }

                        }
                    },

                    {"f",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "1",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R", "R" } },
                                    { "finalMConfig", "f" }
                                }
                            },

                            { "0",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R", "R" } },
                                    { "finalMConfig", "f" }
                                }
                            },

                            { "-",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "P0", "L", "L" } },
                                    { "finalMConfig", "o" }
                                }
                            }


                        }
                    }

                };


        internal static Dictionary<string, Dictionary<string, Dictionary<string, object>>> C10 =

        new Dictionary<string, Dictionary<string, Dictionary<string, object>>> {

                    {"b",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "-",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "P0", "R" } },
                                    { "finalMConfig", "c" }
                                }
                            }
                        }
                    },

                    {"c",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "-",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R" } },
                                    { "finalMConfig", "e" }
                                }
                            }
                        }
                    },

                    {"e",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "-",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "P1", "R" } },
                                    { "finalMConfig", "f" }
                                }
                            }
                        }
                    },

                    {"f",
                        new Dictionary<string, Dictionary<string, object>> {
                            { "-",  new Dictionary<string, object>
                                {
                                    { "operations", new string[] { "R" } },
                                    { "finalMConfig", "b" }
                                }
                            }
                        }
                    }

        };

    }
}
